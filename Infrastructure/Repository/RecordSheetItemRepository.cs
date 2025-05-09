using Domain;
using Domain.Entities.RecordSheet;
using Domain.Models.ErrorInfo;
using Domain.Storage;

namespace Infrastructure.Repository;

public class RecordSheetItemRepository(IStorage<RecordSheetItem> storage, IStorage<RecordSheet> recSheetStorage)
    : RepositoryBase<RecordSheetItem, InvalidRecordSheetItemReason>(storage)
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