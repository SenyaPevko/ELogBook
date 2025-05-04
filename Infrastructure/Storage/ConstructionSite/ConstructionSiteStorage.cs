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
        entity.Image = new Uri(dbo.Image);

        // todo: проставлять тут модельки, после заведения стореджей
        entity.RegistrationSheet = default;
        entity.RecordSheet = default;
        entity.Orders = dbo.Orders.Select(o => new Uri(o)).ToList();
        entity.ConstructionSiteUserRoleIds = default;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(Domain.Entities.ConstructionSite.ConstructionSite entity,
        ConstructionSiteDbo dbo)
    {
        dbo.Name = entity.Name;
        dbo.Description = entity.Description;
        dbo.Address = entity.Address;
        dbo.Image = entity.Image.ToString();
        
        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(Domain.Entities.ConstructionSite.ConstructionSite? existingEntity,
        Domain.Entities.ConstructionSite.ConstructionSite newEntity, ConstructionSiteDbo dbo)
    {
        throw new NotImplementedException();
    }
}