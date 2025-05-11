using Domain.Entities.WorkIssues;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.User;
using Infrastructure.Dbo.WorkIssues;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.WorkIssues;

public class WorkIssueStorage(
    AppDbContext context,
    IRequestContext requestContext,
    IStorage<WorkIssueItem, WorkIssueItemSearchRequest> issueItemStorage)
    : StorageBase<WorkIssue, WorkIssueDbo, WorkIssueSearchRequest>(requestContext)
{
    protected override IMongoCollection<WorkIssueDbo> Collection => context.WorkIssues;

    protected override async Task MapEntityFromDboAsync(WorkIssue entity, WorkIssueDbo dbo)
    {
        var searchRequest = new WorkIssueItemSearchRequest { Ids = dbo.WorkIssueItemIds };
        entity.Id = dbo.Id;
        entity.Items = await issueItemStorage.SearchAsync(searchRequest);
        entity.ConstructionSiteId = dbo.ConstructionSiteId;
    }

    protected override Task MapDboFromEntityAsync(WorkIssue entity, WorkIssueDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.WorkIssueItemIds = entity.Items.Select(item => item.Id).ToList();
        dbo.ConstructionSiteId = entity.ConstructionSiteId;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(WorkIssue? existingEntity, WorkIssue newEntity, WorkIssueDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.WorkIssueItemIds = newEntity.Items.Select(item => item.Id).ToList();
        dbo.ConstructionSiteId = newEntity.ConstructionSiteId;
        
        return Task.CompletedTask;
    }
}