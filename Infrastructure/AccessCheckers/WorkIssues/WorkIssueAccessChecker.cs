using Domain.Entities.WorkIssues;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.WorkIssues;

public class WorkIssueAccessChecker(IRequestContext context) : AccessCheckerBase<WorkIssue>(context)
{
    
}