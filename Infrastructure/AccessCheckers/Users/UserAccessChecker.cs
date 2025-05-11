using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.RequestArgs.Users;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.Users;

public class UserAccessChecker(IRequestContext context) : AccessCheckerBase<User, UserUpdateArgs>(context)
{
    public override async Task<bool> CanUpdate(User entity)
    {
        if (entity.Id != Context.Auth.UserId)
            return false;

        return true;
    }

    public override async Task<bool> CanUpdate(UserUpdateArgs updateArgs, User oldEntity, User newEntity)
    {
        if (updateArgs.OrganizationId is not null)
            return context.Auth.Role is UserRole.Admin;

        return true;
    }
}