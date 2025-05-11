using Domain.Entities.ConstructionSite;
using Domain.Entities.Roles;
using Domain.Entities.WorkIssues;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Domain.RequestArgs.WorkIssueItems;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.WorkIssues;

public class WorkIssueItemAccessChecker(
    IRequestContext context,
    IRepository<ConstructionSite, InvalidConstructionSiteReason, ConstructionSiteSearchRequest> constructionSiteStorage)
    : AccessCheckerBase<WorkIssueItem, WorkIssueItemUpdateArgs>(context)
{
    public override async Task<bool> CanCreate(WorkIssueItem entity)
    {
        var userRoles = await GetUserRoleTypes(entity);

        return userRoles.Any(r => r is ConstructionSiteUserRoleType.Customer or ConstructionSiteUserRoleType.Operator);
    }

    public override async Task<bool> CanUpdate(WorkIssueItem entity)
    {
        var userRoles = await GetUserRoleTypes(entity);

        return userRoles.Contains(ConstructionSiteUserRoleType.AuthorSupervision);
    }
    
    private async Task<List<ConstructionSiteUserRoleType>> GetUserRoleTypes(WorkIssueItem entity)
    {
        // todo: небезопасный First, хотя логичный - нужно переписывать логику валидации зависимостей и связанности, чтобы такой фигни не было
        var constructionSite = (await constructionSiteStorage.SearchAsync(new ConstructionSiteSearchRequest
            { WorkIssueId = entity.WorkIssueId }, default)).First();

        return constructionSite.GetUserRoleTypes(Context);
    }
}