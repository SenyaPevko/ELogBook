using Domain.Dtos.WorkIssue;
using Domain.Entities.WorkIssues;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.WorkIssueItems;

public class SearchWorkIssueItemCommand(IRepository<WorkIssueItem> repository)
    : SearchCommandBase<WorkIssueItemDto, WorkIssueItem>(repository)
{
    protected override async Task<WorkIssueItemDto> MapToDtoAsync(WorkIssueItem entity) => await entity.ToDto();
}