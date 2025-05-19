using Domain.Dtos.WorkIssue;
using Domain.Entities.WorkIssues;
using Domain.RequestArgs.WorkIssueItems;
using ELogBook.Controllers.Base;

namespace ELogBook;

public class WorkIssueItemsController
    : CreatableEntityControllerBase<WorkIssueItemDto, WorkIssueItem, WorkIssueItemCreationArgs,
        WorkIssueItemUpdateArgs, InvalidWorkIssueItemReason, WorkIssueItemSearchRequest>
{
}