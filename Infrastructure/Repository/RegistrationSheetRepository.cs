using Domain;
using Domain.Entities.RegistrationSheet;
using Domain.Models.ErrorInfo;
using Domain.Storage;

namespace Infrastructure.Repository;

public class RegistrationSheetRepository(
    IStorage<RegistrationSheet> storage,
    IStorage<RegistrationSheetItem> regItemStorage)
    : RepositoryBase<RegistrationSheet, InvalidRegistrationSheetReason>(storage)
{
    protected override async Task ValidateCreationAsync(
        RegistrationSheet entity,
        IWriteContext<InvalidRegistrationSheetReason> writeContext,
        CancellationToken cancellationToken)
    {
        foreach (var item in entity.Items)
        {
            if (await regItemStorage.GetByIdAsync(item.Id) is null)
                writeContext.AddInvalidData(new ErrorDetail<InvalidRegistrationSheetReason>
                {
                    Path = nameof(entity.Items),
                    Reason = InvalidRegistrationSheetReason.ReferenceNotFound,
                    Value = item.Id.ToString()
                });
        }
    }
}