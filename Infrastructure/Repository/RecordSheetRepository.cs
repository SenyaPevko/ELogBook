using Domain;
using Domain.Entities.RecordSheet;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.RecordSheets;
using Domain.Storage;

namespace Infrastructure.Repository;

public class RecordSheetRepository(
    IStorage<RecordSheet, RecordSheetSearchRequest> storage,
    IStorage<RecordSheetItem> recItemStorage)
    : RepositoryBase<RecordSheet, InvalidRecordSheetReason, RecordSheetSearchRequest>(storage)
{
    protected override async Task ValidateCreationAsync(
        RecordSheet entity,
        IWriteContext<InvalidRecordSheetReason> writeContext,
        CancellationToken cancellationToken)
    {
        foreach (var item in entity.Items)
            if (await recItemStorage.GetByIdAsync(item.Id) is null)
                writeContext.AddInvalidData(new ErrorDetail<InvalidRecordSheetReason>
                {
                    Path = nameof(entity.Items),
                    Reason = InvalidRecordSheetReason.ReferenceNotFound,
                    Value = item.Id.ToString()
                });
    }

    protected override Task ValidateUpdateAsync(RecordSheet oldEntity, RecordSheet newEntity,
        IWriteContext<InvalidRecordSheetReason> writeContext,
        CancellationToken cancellationToken)
    {
        // todo: нужно проверять элементы на существование
        return Task.CompletedTask;
    }
}