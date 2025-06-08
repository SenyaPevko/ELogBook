using Domain.Entities.WorkIssues;
using Domain.RequestArgs.WorkIssueItems;

namespace Domain.AccessChecker;

public interface IWorkIssueItemAccessChecker
    : IAccessChecker<WorkIssueItem, WorkIssueItemUpdateArgs>,
        IEntityUnderConstructionSiteAccessChecker<WorkIssueItemUpdateArgs>
{
    
}