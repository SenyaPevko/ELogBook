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