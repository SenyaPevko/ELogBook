using Domain.AccessChecker;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.RequestArgs.Users;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.Users;

public class UserAccessChecker(IRequestContext context) 
    : AccessCheckerBase<User, UserUpdateArgs>(context), IUserAccessChecker
{
    public override async Task<bool> CanUpdate(User entity)
    {
        if (entity.Id != Context.Auth.UserId)
            return false;

        return true;
    }

    public override async Task<bool> CanUpdate(UserUpdateArgs updateArgs, User oldEntity, User newEntity)
    {
        var canUpdateOrganization = updateArgs.OrganizationId is null || CanUpdateOrganization();
        var canUpdateUserRole = updateArgs.UserRole is null || CanUpdateUserRole();
        
        return canUpdateUserRole && canUpdateOrganization; 
    }

    public bool CanUpdateOrganization()
    {
        return context.Auth.Role is UserRole.Admin;
    }
    
    public bool CanUpdateUserRole()
    {
        return context.Auth.Role is UserRole.Admin;
    }
}