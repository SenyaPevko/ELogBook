using Domain.AccessChecker;
using Domain.Dtos.WorkIssue;
using Domain.Entities.WorkIssues;
using Domain.Repository;
using Domain.RequestArgs.WorkIssueItems;
using Infrastructure.Commands.Base;
using Infrastructure.Context;

namespace Infrastructure.Commands.WorkIssueItems;

public class CreateWorkIssueItemCommand(
    IRepository<WorkIssueItem, InvalidWorkIssueItemReason> repository,
    IRequestContext context,
    IAccessChecker<WorkIssueItem> accessChecker)
    : CreateCommandBase<WorkIssueItemDto, WorkIssueItem, WorkIssueItemCreationArgs,
        InvalidWorkIssueItemReason>(repository, accessChecker)
{
    protected override async Task<WorkIssueItemDto> MapToDtoAsync(WorkIssueItem entity) => await entity.ToDto();

    protected override Task<WorkIssueItem> MapToEntityAsync(WorkIssueItemCreationArgs args) =>
        Task.FromResult(new WorkIssueItem
        {
            Id = args.Id,
            WorkIssueId = args.WorkIssueId,
            Question = args.Question,
            QuestionDate = context.RequestTime.DateTime,
        });
}