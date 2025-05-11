using Domain.Entities.ConstructionSite;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.Roles;
using Domain.Repository;
using Domain.RequestArgs.RegistrationSheetItems;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.RegistrationSheets;

public class RegistrationSheetItemAccessChecker(
    IRequestContext context,
    IRepository<ConstructionSite, InvalidConstructionSiteReason, ConstructionSiteSearchRequest> constructionSiteStorage)
    : AccessCheckerBase<RegistrationSheetItem, RegistrationSheetItemUpdateArgs>(context)
{
    public override async Task<bool> CanCreate(RegistrationSheetItem entity)
    {
        var userRoles = await GetUserRoleTypes(entity);

        return userRoles.Contains(ConstructionSiteUserRoleType.AuthorSupervision);
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
}