using Domain;
using Domain.Entities.RegistrationSheet;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.RegistrationSheetItems;
using Domain.Storage;

namespace Infrastructure.Repository;

public class RegistrationSheetItemRepository(
    IStorage<RegistrationSheetItem, RegistrationSheetItemSearchRequest> storage,
    IStorage<RegistrationSheet> regSheetStorage)
    : RepositoryBase<RegistrationSheetItem, InvalidRegistrationSheetItemReason, RegistrationSheetItemSearchRequest>(
        storage)
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
                Reason = InvalidRegistrationSheetItemReason.ReferenceNotFound,
                Value = entity.RegistrationSheetId.ToString()
            });
    }

    protected override Task ValidateUpdateAsync(RegistrationSheetItem oldEntity, RegistrationSheetItem newEntity,
        IWriteContext<InvalidRegistrationSheetItemReason> writeContext,
        CancellationToken cancellationToken)
    {
        // todo: нужно проверять элементы на существование
        return Task.CompletedTask;
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