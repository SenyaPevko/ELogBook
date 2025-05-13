using Domain.Entities.ConstructionSite;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Infrastructure.Context;
using Utils;

namespace Infrastructure.AccessCheckers.ConstructionSites;

public class ConstructionSiteAccessChecker(IRequestContext context, IRepository<User> userRepository)
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

        var canUpdateOrders = updateArgs.Orders is null || await CanUpdateOrders(newEntity);
        var canUpdateName = updateArgs.Name is null || Context.Auth.Role is UserRole.Admin;
        var canUpdateDescription = updateArgs.Description is null || Context.Auth.Role is UserRole.Admin;
        var canUpdateAddress = updateArgs.Address is null || Context.Auth.Role is UserRole.Admin;
        var canUpdateUserRoles = updateArgs.UserRoles is null || Context.Auth.Role is UserRole.Admin;

        return canUpdateOrders && canUpdateName && canUpdateDescription && canUpdateAddress &&
               canUpdateUserRoles;
    }

    private async Task<bool> CanUpdateOrders(ConstructionSite entity)
    {
        var user = await userRepository.GetByIdAsync(Context.Auth.UserId!.Value, default);

        return user!.OrganizationId == entity.OrganizationId || user.OrganizationId == entity.SubOrganizationId;
    }
}