using Domain.Entities.Users;
using Domain.Entities.WorkIssues;
using Domain.RequestArgs.WorkIssueItems;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.WorkIssues;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.WorkIssues;

public class WorkIssueItemStorage(AppDbContext context, IRequestContext requestContext, IStorage<User> userStorage)
    : StorageBase<WorkIssueItem, WorkIssueItemDbo, WorkIssueItemSearchRequest>(requestContext)
{
    protected override IMongoCollection<WorkIssueItemDbo> Collection => context.WorkIssueItems;

    protected override async Task MapEntityFromDboAsync(WorkIssueItem entity, WorkIssueItemDbo dbo)
    {
        entity.Id = dbo.Id;
        entity.Question = dbo.Question;
        entity.QuestionedBy = (await userStorage.GetByIdAsync(dbo.CreatedByUserId!.Value))!;
        entity.Answer = dbo.Answer;
        entity.AnsweredBy =  dbo.AnswerUserId is null ? null : await userStorage.GetByIdAsync(dbo.AnswerUserId!.Value);
        entity.AnswerDate = dbo.AnswerDate;
        entity.QuestionDate = dbo.QuestionDate;
        entity.WorkIssueId = dbo.WorkIssueId;
        entity.AnswerUserId = dbo.AnswerUserId;
    }

    protected override Task MapDboFromEntityAsync(WorkIssueItem entity, WorkIssueItemDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.Question = entity.Question;
        dbo.Answer = entity.Answer;
        dbo.AnswerDate = entity.AnswerDate;
        dbo.QuestionDate = entity.QuestionDate;
        dbo.WorkIssueId = entity.WorkIssueId;
        dbo.AnswerUserId = entity.AnswerUserId;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(WorkIssueItem? existingEntity, WorkIssueItem newEntity,
        WorkIssueItemDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.Question = newEntity.Question;
        dbo.Answer = newEntity.Answer;
        dbo.AnswerDate = newEntity.AnswerDate;
        dbo.QuestionDate = newEntity.QuestionDate;
        dbo.WorkIssueId = newEntity.WorkIssueId;
        dbo.AnswerUserId = newEntity.AnswerUserId;

        return Task.CompletedTask;
    }
}