using Domain.AccessChecker;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Roles;
using Domain.Entities.WorkIssues;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Domain.RequestArgs.WorkIssueItems;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.WorkIssues;

public class WorkIssueItemAccessChecker(
    IRequestContext context,
    IRepository<ConstructionSite, InvalidConstructionSiteReason, ConstructionSiteSearchRequest> constructionSiteStorage)
    : AccessCheckerBase<WorkIssueItem, WorkIssueItemUpdateArgs>(context), IWorkIssueItemAccessChecker
{
    public override async Task<bool> CanCreate(WorkIssueItem entity)
    {
        var userRoles = await GetUserRoleTypes(entity);

        return CanCreate(userRoles);
    }

    public override async Task<bool> CanUpdate(WorkIssueItem entity)
    {
        var userRoles = await GetUserRoleTypes(entity);

        return CanUpdate(userRoles);
    }

    private async Task<List<ConstructionSiteUserRoleType>> GetUserRoleTypes(WorkIssueItem entity)
    {
        // todo: небезопасный First, хотя логичный - нужно переписывать логику валидации зависимостей и связанности, чтобы такой фигни не было
        var constructionSite = (await constructionSiteStorage.SearchAsync(new ConstructionSiteSearchRequest
            { WorkIssueId = entity.WorkIssueId }, default)).First();

        return constructionSite.GetUserRoleTypes(Context);
    }

    public bool CanCreate(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Any(r => r is ConstructionSiteUserRoleType.Customer or ConstructionSiteUserRoleType.Operator);
    }

    public bool CanRead(List<ConstructionSiteUserRoleType> userRoles)
    {
        return true;
    }

    public bool CanUpdate(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Contains(ConstructionSiteUserRoleType.AuthorSupervision);
    }

    public bool CanUpdate(WorkIssueItemUpdateArgs updateArgs, List<ConstructionSiteUserRoleType> userRoles)
    {
        return true;
    }

    public async Task<List<ConstructionSiteUserRoleType>> GetUserRoleTypes(Guid constructionSiteId)
    {
        var constructionSite = await constructionSiteStorage.GetByIdAsync(constructionSiteId, default);

        return constructionSite is null ? [] : constructionSite.GetUserRoleTypes(Context);
    }
}