using Domain.Entities.Organization;
using Domain.Entities.Roles;
using Domain.RequestArgs.Organizations;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.Organizations;

public class OrganizationAccessChecker(IRequestContext context)
    : AccessCheckerBase<Organization, OrganizationUpdateArgs>(context)
{
    public override async Task<bool?> CanRead()
    {
        var baseResult = await base.CanRead();
        if (baseResult is not null)
            return baseResult;

        return Context.Auth.Role is UserRole.Admin;
    }

    public override async Task<bool?> CanCreate()
    {
        var baseResult = await base.CanCreate();
        if (baseResult is not null)
            return baseResult;

        return Context.Auth.Role is UserRole.Admin;
    }

    public override async Task<bool?> CanUpdate()
    {
        var baseResult = await base.CanUpdate();
        if (baseResult is not null)
            return baseResult;

        return Context.Auth.Role is UserRole.Admin;
    }
}