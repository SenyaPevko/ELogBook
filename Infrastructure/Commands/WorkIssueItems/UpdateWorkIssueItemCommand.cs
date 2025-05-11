using Domain.AccessChecker;
using Domain.Dtos.WorkIssue;
using Domain.Entities.WorkIssues;
using Domain.Repository;
using Domain.RequestArgs.WorkIssueItems;
using Infrastructure.Commands.Base;
using Infrastructure.Context;

namespace Infrastructure.Commands.WorkIssueItems;

public class UpdateWorkIssueItemCommand(
    IRepository<WorkIssueItem, InvalidWorkIssueItemReason> repository,
    IRequestContext context,
    IAccessChecker<WorkIssueItem, WorkIssueItemUpdateArgs> accessChecker)
    : UpdateCommandBase<WorkIssueItemDto, WorkIssueItem, WorkIssueItemUpdateArgs,
        InvalidWorkIssueItemReason>(repository, accessChecker)
{
    protected override async Task<WorkIssueItemDto> MapToDtoAsync(WorkIssueItem entity)
    {
        return await entity.ToDto();
    }

    protected override Task ApplyUpdatesAsync(WorkIssueItem entity, WorkIssueItemUpdateArgs args)
    {
        if (args.Question is not null) entity.Question = args.Question;
        if (args.Answer is not null)
        {
            entity.Answer = args.Answer;
            entity.AnswerDate = context.RequestTime.DateTime;
        }

        return Task.CompletedTask;
    }
}