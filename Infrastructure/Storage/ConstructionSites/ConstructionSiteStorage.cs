using Domain.Entities.ConstructionSite;
using Infrastructure.Context;
using Infrastructure.Dbo.ConstructionSite;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.ConstructionSites;

public class ConstructionSiteStorage(AppDbContext dbContext, IRequestContext requestContext)
    : StorageBase<ConstructionSite, ConstructionSiteDbo>(dbContext, requestContext)
{
    private readonly AppDbContext _dbContext = dbContext;
    protected override IMongoCollection<ConstructionSiteDbo> Collection => _dbContext.ConstructionSites;

    protected override Task MapEntityFromDboAsync(ConstructionSite entity,
        ConstructionSiteDbo dbo)
    {
        entity.Id = dbo.Id;
        entity.Name = dbo.Name;
        entity.Description = dbo.Description;
        entity.Address = dbo.Address;
        entity.Image = new Uri(dbo.Image);

        // todo: проставлять тут модельки, после заведения стореджей
        entity.RegistrationSheet = default;
        entity.RecordSheet = default;
        entity.Orders = dbo.Orders.Select(o => new Uri(o)).ToList();
        entity.ConstructionSiteUserRoles = default;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(ConstructionSite entity,
        ConstructionSiteDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.Name = entity.Name;
        dbo.Description = entity.Description;
        dbo.Address = entity.Address;
        dbo.Image = entity.Image.ToString();

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(ConstructionSite? existingEntity,
        ConstructionSite newEntity, ConstructionSiteDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.Name = newEntity.Name;
        dbo.Description = newEntity.Description;
        dbo.Address = newEntity.Address;
        dbo.Image = newEntity.Image.ToString();

        return Task.CompletedTask;
    }
}