using Domain;
using Domain.Entities.RegistrationSheet;
using Domain.Storage;

namespace Infrastructure.Repository;

public class RegistrationSheetItemRepository(IStorage<RegistrationSheetItem> storage)
    : RepositoryBase<RegistrationSheetItem, InvalidRegistrationSheetItemReason>(storage)
{
    // todo: тут нужен некий afterWrite, чтобы после создания добавлять в RegistrationSheet
    
    protected override Task ValidateCreationAsync(RegistrationSheetItem entity,
        IWriteContext<InvalidRegistrationSheetItemReason> writeContext,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}