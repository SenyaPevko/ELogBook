using Domain.AccessChecker;
using Domain.Entities.ConstructionSite;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.Roles;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Domain.RequestArgs.RegistrationSheetItems;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.RegistrationSheets;

public class RegistrationSheetItemAccessChecker(
    IRequestContext context,
    IRepository<ConstructionSite, InvalidConstructionSiteReason, ConstructionSiteSearchRequest> constructionSiteStorage)
    : AccessCheckerBase<RegistrationSheetItem, RegistrationSheetItemUpdateArgs>(context),
        IRegistrationSheetItemAccessChecker
{
    public override async Task<bool> CanCreate(RegistrationSheetItem entity)
    {
        var userRoles = await GetUserRoleTypes(entity);

        return CanCreate(userRoles);
    }

    public override async Task<bool> CanUpdate(RegistrationSheetItem entity)
    {
        var userRoles = await GetUserRoleTypes(entity);
        var passedTime = Context.RequestTime - entity.UpdateInfo.CreatedAt;

        return userRoles.Any(r => r is ConstructionSiteUserRoleType.AuthorSupervision) && passedTime.TotalDays < 5;
    }

    private async Task<List<ConstructionSiteUserRoleType>> GetUserRoleTypes(RegistrationSheetItem entity)
    {
        // todo: небезопасный First, хотя логичный - нужно переписывать логику валидации зависимостей и связанности, чтобы такой фигни не было
        var constructionSite = (await constructionSiteStorage.SearchAsync(new ConstructionSiteSearchRequest
            { RegistrationSheetId = entity.RegistrationSheetId }, default)).First();

        return constructionSite.GetUserRoleTypes(Context);
    }

    public bool CanCreate(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Contains(ConstructionSiteUserRoleType.AuthorSupervision);
    }

    public bool CanRead(List<ConstructionSiteUserRoleType> userRoles)
    {
        return true;
    }

    public bool CanUpdate(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Any(r => r is ConstructionSiteUserRoleType.AuthorSupervision);
    }

    public bool CanUpdate(RegistrationSheetItemUpdateArgs updateArgs, List<ConstructionSiteUserRoleType> userRoles)
    {
        return true;
    }

    public async Task<List<ConstructionSiteUserRoleType>> GetUserRoleTypes(Guid constructionSiteId)
    {
        var constructionSite = await constructionSiteStorage.GetByIdAsync(constructionSiteId, default);

        return constructionSite is null ? [] : constructionSite.GetUserRoleTypes(Context);
    }
}