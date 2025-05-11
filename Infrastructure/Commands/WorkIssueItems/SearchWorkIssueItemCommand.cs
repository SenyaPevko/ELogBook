using Domain.AccessChecker;
using Domain.Dtos.WorkIssue;
using Domain.Entities.WorkIssues;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.WorkIssueItems;

public class SearchWorkIssueItemCommand(
    IRepository<WorkIssueItem, InvalidWorkIssueItemReason, WorkIssueItemSearchRequest> repository,
    IAccessChecker<WorkIssueItem> accessChecker)
    : SearchCommandBase<WorkIssueItemDto, WorkIssueItem, InvalidWorkIssueItemReason, WorkIssueItemSearchRequest>(
        repository, accessChecker)
{
    protected override async Task<WorkIssueItemDto> MapToDtoAsync(WorkIssueItem entity)
    {
        return await entity.ToDto();
    }
}