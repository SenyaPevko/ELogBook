using Domain.AccessChecker;
using Domain.Dtos.WorkIssue;
using Domain.Entities.WorkIssues;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.WorkIssueItems;

public class GetWorkIssueItemCommand(IRepository<WorkIssueItem> repository, IAccessChecker<WorkIssueItem> accessChecker)
    : GetCommandBase<WorkIssueItemDto, WorkIssueItem>(repository, accessChecker)
{
    protected override async Task<WorkIssueItemDto> MapToDtoAsync(WorkIssueItem entity) => await entity.ToDto();
}