using Domain.AccessChecker;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Permissions;
using Domain.Repository;
using Domain.RequestArgs.Users;
using Infrastructure.Permissions.Base;

namespace Infrastructure.Permissions;

public class UserPermissionService(
    IUserAccessChecker accessChecker,
    IRepository<User> repository)
    : EntityPermissionServiceBase<User, UserUpdateArgs,
        UserPermission>(accessChecker, repository)
{
    protected override async Task FillPermissions(Guid? entityId, UserPermission permissions, CancellationToken cancellationToken)
    {
        permissions.CanUpdateOrganization = accessChecker.CanUpdateOrganization();
        permissions.CanUpdateUserRole = accessChecker.CanUpdateUserRole();
    }
    
    protected override UserUpdateArgs FillUpdateArgs() =>
        new()
        {
            Name = string.Empty,
            Surname = string.Empty,
            Patronymic = string.Empty,
            Phone = string.Empty,
            OrganizationId = Guid.Empty,
            UserRole = UserRole.Unknown
        };
}