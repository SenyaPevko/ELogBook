using Domain;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Notifications;
using Domain.Entities.RecordSheet;
using Domain.Entities.Roles;
using Domain.FileStorage;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Domain.RequestArgs.RecordSheetItems;
using Domain.Storage;
using Infrastructure.WriteContext;
using MongoDB.Bson;

namespace Infrastructure.Repository;

public class RecordSheetItemRepository(
    IStorage<RecordSheetItem, RecordSheetItemSearchRequest> storage,
    IStorage<RecordSheet> recSheetStorage,
    IFileStorageService fileStorage,
    IRepository<RecordSheetItemNotification, InvalidNotificationReason> notificationRepository,
    IStorage<ConstructionSite, ConstructionSiteSearchRequest> constructionSiteStorage)
    : RepositoryBase<RecordSheetItem, InvalidRecordSheetItemReason, RecordSheetItemSearchRequest>(storage)
{
    protected override async Task ValidateCreationAsync(RecordSheetItem entity,
        IWriteContext<InvalidRecordSheetItemReason> writeContext, CancellationToken cancellationToken)
    {
        var recSheet = await recSheetStorage.GetByIdAsync(entity.RecordSheetId);
        if (recSheet is null)
            writeContext.AddInvalidData(new ErrorDetail<InvalidRecordSheetItemReason>
            {
                Path = nameof(entity.RecordSheetId),
                Reason = InvalidRecordSheetItemReason.ReferenceNotFound,
                Value = entity.RecordSheetId.ToString()
            });

        await ValidateFiles(entity.DeviationFilesIds, nameof(entity.DeviationFilesIds), writeContext);
        await ValidateFiles(entity.DirectionFilesIds, nameof(entity.DirectionFilesIds), writeContext);
    }

    private async Task ValidateFiles(List<ObjectId> fileIds, string path,
        IWriteContext<InvalidRecordSheetItemReason> writeContext)
    {
        foreach (var fileId in fileIds)
        {
            var metadata = await fileStorage.GetFileInfoAsync(fileId);
            if (metadata is null)
                writeContext.AddInvalidData(new ErrorDetail<InvalidRecordSheetItemReason>
                {
                    Path = $"{path}/{fileId}",
                    Reason = InvalidRecordSheetItemReason.ReferenceNotFound,
                    Value = fileId.ToString()
                });
        }
    }

    protected override Task ValidateUpdateAsync(
        RecordSheetItem oldEntity,
        RecordSheetItem newEntity,
        IWriteContext<InvalidRecordSheetItemReason> writeContext,
        CancellationToken cancellationToken)
    {
        // todo: вообще в теории здесь нужно проверять на наличие в базе RepresentativeId и ComplianceNoteUserId,
        // но мы вроде где-то уже проверяем это, поэтому можно потом сюда написать, и подумать нужно ли как-то
        // синхронить проверки наличия объекта в базе на разных уровнях бэка 

        if (oldEntity.Deviations != newEntity.Deviations &&
            (newEntity.RepresentativeId is not null || newEntity.ComplianceNoteUserId is not null))
            writeContext.AddInvalidData(new ErrorDetail<InvalidRecordSheetItemReason>
            {
                Path = nameof(RecordSheetItem.Deviations),
                Reason = InvalidRecordSheetItemReason.RecordHasBeenSigned,
                Value = oldEntity.Deviations
            });

        if (oldEntity.Directions != newEntity.Directions &&
            (newEntity.RepresentativeId is not null || newEntity.ComplianceNoteUserId is not null))
            writeContext.AddInvalidData(new ErrorDetail<InvalidRecordSheetItemReason>
            {
                Path = nameof(RecordSheetItem.Directions),
                Reason = InvalidRecordSheetItemReason.RecordHasBeenSigned,
                Value = oldEntity.Directions
            });

        return Task.CompletedTask;
    }

    protected override async Task AfterCreateAsync(
        RecordSheetItem entity,
        IWriteContext<InvalidRecordSheetItemReason> writeContext,
        CancellationToken cancellationToken)
    {
        var recSheet = await recSheetStorage.GetByIdAsync(entity.RecordSheetId);
        recSheet!.Items.Add(entity);
        await recSheetStorage.UpdateAsync(recSheet);

        await NotifyAboutCreation(entity, writeContext, cancellationToken);
    }

    private async Task NotifyAboutCreation(RecordSheetItem entity,
        IWriteContext<InvalidRecordSheetItemReason> writeContext,
        CancellationToken cancellationToken)
    {
        var constructionSite = (await constructionSiteStorage.SearchAsync(new ConstructionSiteSearchRequest
            { RecordSheetId = entity.RecordSheetId })).FirstOrDefault();
        if (constructionSite is null)
        {
            writeContext.AddInvalidData(new ErrorDetail<InvalidRecordSheetItemReason>
            {
                Path = nameof(entity.RecordSheetId),
                Reason = InvalidRecordSheetItemReason.ReferenceNotFound,
                Value = entity.RecordSheetId.ToString()
            });
            return;
        }

        var targetUsers = constructionSite.ConstructionSiteUserRoles
            .Where(x => x.Role is ConstructionSiteUserRoleType.Customer or ConstructionSiteUserRoleType.Operator)
            .Select(x => x.UserId)
            .ToList();
        if (targetUsers.Count == 0) 
            return;

        var notifications = targetUsers.Select(userId => new RecordSheetItemNotification
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = "Новая запись в учетном листе",
            Message = $"Добавлена новая запись в учетный лист для объекта {constructionSite.ShortName}",
            IsRead = false,
            RecordSheetItemId = entity.Id,
            RecordSheetId = entity.RecordSheetId,
            ConstructionSiteId = constructionSite.Id,
        }).ToList();


        var bulkWriteContext = new BulkWriteContext<RecordSheetItemNotification, InvalidNotificationReason>();
        await notificationRepository.AddManyAsync(notifications, bulkWriteContext, cancellationToken);

        if (!bulkWriteContext.IsSuccess)
            writeContext.AddInvalidData(new ErrorDetail<InvalidRecordSheetItemReason>
            {
                Reason = InvalidRecordSheetItemReason.FailedNotifyUsers,
            });
    }
}