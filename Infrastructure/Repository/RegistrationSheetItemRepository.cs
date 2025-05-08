using Domain;
using Domain.Entities.RegistrationSheet;
using Domain.Models.ErrorInfo;
using Domain.Storage;

namespace Infrastructure.Repository;

public class RegistrationSheetItemRepository(IStorage<RegistrationSheetItem> storage, IStorage<RegistrationSheet> regSheetStorage)
    : RepositoryBase<RegistrationSheetItem, InvalidRegistrationSheetItemReason>(storage)
{
    protected override async Task ValidateCreationAsync(RegistrationSheetItem entity,
        IWriteContext<InvalidRegistrationSheetItemReason> writeContext,
        CancellationToken cancellationToken)
    {
        var regSheet = await regSheetStorage.GetByIdAsync(entity.RegistrationSheetId);
        if (regSheet is null)
            writeContext.AddInvalidData(new ErrorDetail<InvalidRegistrationSheetItemReason>
            {
                Path = nameof(entity.RegistrationSheetId),
                Reason = InvalidRegistrationSheetItemReason.ReferenceIsNotFound,
                Value = entity.RegistrationSheetId.ToString()
            });
    }
    
    protected override async Task AfterCreateAsync(
        RegistrationSheetItem entity,
        IWriteContext<InvalidRegistrationSheetItemReason> writeContext,
        CancellationToken cancellationToken)
    {
        var regSheet = await regSheetStorage.GetByIdAsync(entity.RegistrationSheetId);
        regSheet!.Items.Add(entity);
        await regSheetStorage.UpdateAsync(regSheet);
    }
}