using Domain.Entities.ConstructionSite;
using Domain.Entities.Roles;
using Domain.RequestArgs.ConstructionSites;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.ConstructionSites;

public class ConstructionSiteAccessChecker(IRequestContext context)
    : AccessCheckerBase<ConstructionSite, ConstructionSiteUpdateArgs>(context)
{
    public override async Task<bool?> CanCreate()
    {
        var baseResult = await base.CanCreate();
        if (baseResult is not null)
            return baseResult;

        return Context.Auth.Role is UserRole.Admin;
    }

    public override async Task<bool> CanUpdate(
        ConstructionSite entity)
    {
        var userRoles = entity.GetUserRoleTypes(Context);

        return userRoles.Count != 0;
    }

    public override async Task<bool> CanUpdate(
        ConstructionSiteUpdateArgs updateArgs,
        ConstructionSite oldEntity,
        ConstructionSite newEntity)
    {
        var userRoles = oldEntity.GetUserRoleTypes(Context);

        if (userRoles.Count == 0)
            return false;

        var canUpdateOrders = updateArgs.Orders is null || CanUpdateOrders(userRoles);
        var canUpdateName = updateArgs.Name is null || Context.Auth.Role is UserRole.Admin;
        var canUpdateDescription = updateArgs.Description is null || Context.Auth.Role is UserRole.Admin;
        var canUpdateAddress = updateArgs.Address is null || Context.Auth.Role is UserRole.Admin;
        var canUpdateImage = updateArgs.Image is null || Context.Auth.Role is UserRole.Admin;
        var canUpdateUserRoles = updateArgs.UserRoles is null || Context.Auth.Role is UserRole.Admin;

        return canUpdateOrders && canUpdateName && canUpdateDescription && canUpdateAddress && canUpdateImage && canUpdateUserRoles;
    }
    
    private bool CanUpdateOrders(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Any(r =>
            r is ConstructionSiteUserRoleType.Customer or ConstructionSiteUserRoleType.AuthorSupervision);
    }
}