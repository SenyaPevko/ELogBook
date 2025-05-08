using Domain.Entities;
using Domain.Entities.Organization;
using Domain.Entities.Users;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Context;
using Infrastructure.Dbo;
using Infrastructure.Helpers.SearchRequestHelper;
using Infrastructure.Storage.Base;
using Infrastructure.Storage.Users;
using MongoDB.Driver;

namespace Infrastructure.Storage.Organizations;

public class OrganizationStorage(AppDbContext context, IRequestContext requestContext)
    : StorageBase<Organization, OrganizationDbo>(context, requestContext)
{
    private readonly AppDbContext _context = context;
    protected override IMongoCollection<OrganizationDbo> Collection => _context.Organizations;

    protected override Task MapEntityFromDboAsync(Organization entity, OrganizationDbo dbo)
    {
        entity.Id = dbo.Id;
        entity.Name = dbo.Name;
        entity.UserIds = dbo.UserIds;
        
        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(Organization entity, OrganizationDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.Name = entity.Name;
        dbo.UserIds = entity.UserIds;
        
        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(Organization? existingEntity, Organization newEntity,
        OrganizationDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.Name = newEntity.Name;
        dbo.UserIds = newEntity.UserIds;
        
        return Task.CompletedTask;
    }
}