using Infrastructure.Context;
using Infrastructure.Dbo;
using Infrastructure.Storage.Base;

namespace Infrastructure.Storage.ConstructionSite;

public class ConstructionSiteStorage(AppDbContext dbContext)
    : StorageBase<Domain.Entities.ConstructionSite.ConstructionSite, ConstructionSiteDbo>(dbContext)
{
    protected override Task MapEntityFromDboAsync(Domain.Entities.ConstructionSite.ConstructionSite entity,
        ConstructionSiteDbo dbo)
    {
        entity.Name = dbo.Name;
        entity.Description = dbo.Description;
        entity.Address = dbo.Address;
        entity.Image = dbo.Image;

        // todo: проставлять тут модельки, после заведения стореджей
        entity.RegistrationSheet = default;
        entity.RecordSheet = default;
        entity.Orders = dbo.Orders;
        entity.ConstructionSiteUserRoleIds = default;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(Domain.Entities.ConstructionSite.ConstructionSite newEntity,
        ConstructionSiteDbo dbo)
    {
        throw new NotImplementedException();
    }

    protected override Task MapDboFromEntityAsync(Domain.Entities.ConstructionSite.ConstructionSite? existingEntity,
        Domain.Entities.ConstructionSite.ConstructionSite newEntity, ConstructionSiteDbo dbo)
    {
        throw new NotImplementedException();
    }
}