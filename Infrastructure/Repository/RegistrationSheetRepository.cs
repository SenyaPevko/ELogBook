using Domain.Entities.RegistrationSheet;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.RegistrationSheets;
using Domain.Storage;

namespace Infrastructure.Repository;

public class RegistrationSheetRepository(
    IStorage<RegistrationSheet, RegistrationSheetSearchRequest> storage,
    IStorage<RegistrationSheetItem> regItemStorage)
    : RepositoryBase<RegistrationSheet, InvalidRegistrationSheetReason, RegistrationSheetSearchRequest>(storage)
{
    protected override async Task ValidateCreationAsync(
        RegistrationSheet entity,
        IWriteContext<InvalidRegistrationSheetReason> writeContext,
        CancellationToken cancellationToken)
    {
        foreach (var item in entity.Items)
            if (await regItemStorage.GetByIdAsync(item.Id) is null)
                writeContext.AddInvalidData(new ErrorDetail<InvalidRegistrationSheetReason>
                {
                    Path = nameof(entity.Items),
                    Reason = InvalidRegistrationSheetReason.ReferenceNotFound,
                    Value = item.Id.ToString()
                });
    }

    protected override Task ValidateUpdateAsync(RegistrationSheet oldEntity, RegistrationSheet newEntity,
        IWriteContext<InvalidRegistrationSheetReason> writeContext,
        CancellationToken cancellationToken)
    {
        // todo: нужно проверять элементы на существование
        return Task.CompletedTask;
    }
}