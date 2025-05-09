using Domain.Entities.WorkIssues;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Context;
using Infrastructure.Dbo.WorkIssues;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.WorkIssues;

public class WorkIssueItemStorage(AppDbContext context, IRequestContext requestContext)
    : StorageBase<WorkIssueItem, WorkIssueItemDbo, WorkIssueItemSearchRequest>(requestContext)
{
    protected override IMongoCollection<WorkIssueItemDbo> Collection => context.WorkIssueItems;

    protected override Task MapEntityFromDboAsync(WorkIssueItem entity, WorkIssueItemDbo dbo)
    {
        entity.Id = dbo.Id;
        entity.Question = dbo.Question;
        entity.Answer = dbo.Answer;
        entity.AnswerDate = dbo.AnswerDate;
        entity.WorkIssueId = dbo.WorkIssueId;
        
        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(WorkIssueItem entity, WorkIssueItemDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.Question = entity.Question;
        dbo.Answer = entity.Answer;
        dbo.AnswerDate = entity.AnswerDate;
        dbo.WorkIssueId = entity.WorkIssueId;
        
        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(WorkIssueItem? existingEntity, WorkIssueItem newEntity,
        WorkIssueItemDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.Question = newEntity.Question;
        dbo.Answer = newEntity.Answer;
        dbo.AnswerDate = newEntity.AnswerDate;
        dbo.WorkIssueId = newEntity.WorkIssueId;
        
        return Task.CompletedTask;
    }
}