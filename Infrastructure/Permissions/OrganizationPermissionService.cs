using Domain.AccessChecker;
using Domain.Entities.Organization;
using Domain.Permissions;
using Domain.Repository;
using Domain.RequestArgs.Base;
using Domain.RequestArgs.Organizations;
using Infrastructure.Permissions.Base;

namespace Infrastructure.Permissions;

public class OrganizationPermissionService(
    IAccessChecker<Organization, OrganizationUpdateArgs> accessChecker,
    IRepository<Organization> repository)
    : EntityPermissionServiceBase<Organization, OrganizationUpdateArgs,
        OrganizationPermission>(accessChecker, repository)
{
    protected override OrganizationUpdateArgs FillUpdateArgs() =>
        new()
        {
            Name = string.Empty,
            UserIds = new ListUpdateOfId<Guid>
            {
                Add = [Guid.Empty],
                Remove = [Guid.Empty]
            },
        };
}