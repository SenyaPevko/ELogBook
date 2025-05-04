using Domain;
using Domain.Entities.ConstructionSite;
using Domain.Storage;

namespace Infrastructure.Repository;

public class ConstructionSiteRepository(IStorage<ConstructionSite> storage)
    : RepositoryBase<ConstructionSite, InvalidConstructionSiteReason>(storage)
{
    protected override Task ValidateCreationAsync(
        ConstructionSite entity,
        IWriteContext<InvalidConstructionSiteReason> writeContext,
        CancellationToken cancellationToken)
    {
        //todo: мб нужно изображения валидировать ._.

        return Task.CompletedTask;
    }
}