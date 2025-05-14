using Domain;
using Domain.Entities.RecordSheet;
using Domain.FileStorage;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using MongoDB.Bson;

namespace Infrastructure.Repository;

public class RecordSheetItemRepository(
    IStorage<RecordSheetItem, RecordSheetItemSearchRequest> storage,
    IStorage<RecordSheet> recSheetStorage,
    IFileStorageService fileStorage)
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

    private async Task ValidateFiles(List<ObjectId> fileIds, string path, IWriteContext<InvalidRecordSheetItemReason> writeContext)
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
    }
}