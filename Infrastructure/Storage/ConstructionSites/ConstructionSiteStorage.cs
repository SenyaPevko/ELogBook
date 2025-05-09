using Domain.Entities.ConstructionSite;
using Domain.Entities.RecordSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.WorkIssues;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.ConstructionSite;
using Infrastructure.Storage.Base;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Storage.ConstructionSites;

public class ConstructionSiteStorage(
    AppDbContext dbContext,
    IRequestContext requestContext,
    IStorage<RegistrationSheet> regSheetStorage,
    IStorage<RecordSheet> recSheetStorage,
    IStorage<WorkIssue> workIssueStorage)
    : StorageBase<ConstructionSite, ConstructionSiteDbo, ConstructionSiteSearchRequest>(requestContext)
{
    protected override IMongoCollection<ConstructionSiteDbo> Collection => dbContext.ConstructionSites;

    protected override async Task MapEntityFromDboAsync(ConstructionSite entity,
        ConstructionSiteDbo dbo)
    {
        entity.Id = dbo.Id;
        entity.Name = dbo.Name;
        entity.Description = dbo.Description;
        entity.Address = dbo.Address;
        entity.Image = new Uri(dbo.Image);
        entity.Orders = dbo.Orders;
        entity.ConstructionSiteUserRoles = dbo.ConstructionSiteUserRoles;
        
        entity.RegistrationSheet = (await regSheetStorage.GetByIdAsync(dbo.RegistrationSheetId))!;
        entity.RecordSheet = (await recSheetStorage.GetByIdAsync(dbo.RecordSheetId))!;
        entity.WorkIssue = (await workIssueStorage.GetByIdAsync(dbo.WorkIssueId))!;
    }

    protected override Task MapDboFromEntityAsync(ConstructionSite entity,
        ConstructionSiteDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.Name = entity.Name;
        dbo.Description = entity.Description;
        dbo.Address = entity.Address;
        dbo.Image = entity.Image.ToString();
        dbo.Orders = entity.Orders;
        dbo.ConstructionSiteUserRoles = entity.ConstructionSiteUserRoles;
        dbo.RegistrationSheetId = entity.RegistrationSheet.Id;
        dbo.RecordSheetId = entity.RecordSheet.Id;
        dbo.WorkIssueId = entity.WorkIssue.Id;

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
        dbo.Orders = newEntity.Orders;
        dbo.ConstructionSiteUserRoles = newEntity.ConstructionSiteUserRoles;
        dbo.RegistrationSheetId = newEntity.RegistrationSheet.Id;
        dbo.RecordSheetId = newEntity.RecordSheet.Id;
        dbo.WorkIssueId = newEntity.WorkIssue.Id;

        return Task.CompletedTask;
    }

    protected override List<FilterDefinition<ConstructionSiteDbo>> BuildSpecificFilters(
        ConstructionSiteSearchRequest request)
    {
        var filters = new List<FilterDefinition<ConstructionSiteDbo>>();
        var builder = Builders<ConstructionSiteDbo>.Filter;

        if (!string.IsNullOrEmpty(request.Name))
        {
            filters.Add(builder.Regex(x => x.Name, 
                new BsonRegularExpression(request.Name, "i")));
        }
        
        if (!string.IsNullOrEmpty(request.Address))
        {
            filters.Add(builder.Regex(x => x.Address, 
                new BsonRegularExpression(request.Address, "i")));
        }
        
        if (request.UserRoleUserId.HasValue)
        {
            var innerBuilder = Builders<ConstructionSiteUserRole>.Filter;
            filters.Add(builder.ElemMatch(
                x => x.ConstructionSiteUserRoles,
                innerBuilder.And(
                    innerBuilder.Eq(x => x.UserId, request.UserRoleUserId.Value)
                )));
        }

        return filters;
    }
}