using Domain.Entities.WorkIssues;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.WorkIssues;
using Infrastructure.Helpers.SearchRequestHelper;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.WorkIssues;

public class WorkIssueStorage(AppDbContext context, IRequestContext requestContext, IStorage<WorkIssueItem> issueItemStorage) 
    : StorageBase<WorkIssue, WorkIssueDbo>(context, requestContext)
{
    private readonly AppDbContext _context = context;
    protected override IMongoCollection<WorkIssueDbo> Collection => _context.WorkIssues;
    protected override async Task MapEntityFromDboAsync(WorkIssue entity, WorkIssueDbo dbo)
    {
        var searchRequest = new SearchRequest().WhereIn<WorkIssueItem, Guid>(item => item.Id, dbo.WorkIssueItemIds);
        entity.Id = dbo.Id;
        entity.Items = await issueItemStorage.SearchAsync(searchRequest);
    }

    protected override Task MapDboFromEntityAsync(WorkIssue entity, WorkIssueDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.WorkIssueItemIds = entity.Items.Select(item => item.Id).ToList();
        
        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(WorkIssue? existingEntity, WorkIssue newEntity, WorkIssueDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.WorkIssueItemIds = newEntity.Items.Select(item => item.Id).ToList();
        
        return Task.CompletedTask;
    }
}