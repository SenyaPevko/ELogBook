using Domain;
using Domain.Entities.RecordSheet;
using Domain.Models.ErrorInfo;
using Domain.Storage;

namespace Infrastructure.Repository;

public class RecordSheetRepository(IStorage<RecordSheet> storage, IStorage<RecordSheetItem> recItemStorage)
    : RepositoryBase<RecordSheet, InvalidRecordSheetReason>(storage)
{
    protected override async Task ValidateCreationAsync(
        RecordSheet entity,
        IWriteContext<InvalidRecordSheetReason> writeContext,
        CancellationToken cancellationToken)
    {
        foreach (var item in entity.Items)
        {
            if (await recItemStorage.GetByIdAsync(item.Id) is null)
                writeContext.AddInvalidData(new ErrorDetail<InvalidRecordSheetReason>
                {
                    Path = nameof(entity.Items),
                    Reason = InvalidRecordSheetReason.ReferenceNotFound,
                    Value = item.Id.ToString()
                });
        }
    }
}