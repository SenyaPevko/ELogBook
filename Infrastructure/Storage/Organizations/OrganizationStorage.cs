using Domain.Entities.Organization;
using Domain.RequestArgs.Organizations;
using Infrastructure.Context;
using Infrastructure.Dbo;
using Infrastructure.Storage.Base;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Storage.Organizations;

public class OrganizationStorage(AppDbContext context, IRequestContext requestContext)
    : StorageBase<Organization, OrganizationDbo, OrganizationSearchRequest>(requestContext)
{
    protected override IMongoCollection<OrganizationDbo> Collection => context.Organizations;

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

    protected override List<FilterDefinition<OrganizationDbo>> BuildSpecificFilters(
        OrganizationSearchRequest request)
    {
        var filters = new List<FilterDefinition<OrganizationDbo>>();
        var builder = Builders<OrganizationDbo>.Filter;

        if (!string.IsNullOrEmpty(request.Name))
            filters.Add(builder.Regex(x => x.Name,
                new BsonRegularExpression(request.Name, "i")));

        return filters;
    }
    
    protected override bool IsSpecificSearchRequestEmpty(OrganizationSearchRequest request)
    {
        return request.Name is null;
    }
}