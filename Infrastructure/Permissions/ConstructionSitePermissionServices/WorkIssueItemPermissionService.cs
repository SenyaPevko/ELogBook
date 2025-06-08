using Domain.AccessChecker;
using Domain.Entities.WorkIssues;
using Domain.Permissions.ConstructionSite;
using Domain.RequestArgs.WorkIssueItems;

namespace Infrastructure.Permissions.ConstructionSitePermissionServices;

public class WorkIssueItemPermissionService(
    IWorkIssueItemAccessChecker accessChecker)
    : EntityUnderConstructionSitePermissionServiceBase<WorkIssueItem, WorkIssueItemUpdateArgs,
        WorkIssueItemPermission>(accessChecker, accessChecker)
{
    protected override WorkIssueItemUpdateArgs FillUpdateArgs() =>
        new()
        {
            Question = string.Empty,
            Answer = string.Empty,
        };
}