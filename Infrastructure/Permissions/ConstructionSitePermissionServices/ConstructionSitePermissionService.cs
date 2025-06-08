using Domain.AccessChecker;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Roles;
using Domain.Permissions.ConstructionSite;
using Domain.Repository;
using Domain.RequestArgs.Base;
using Domain.RequestArgs.ConstructionSites;
using Infrastructure.Permissions.Base;

namespace Infrastructure.Permissions.ConstructionSitePermissionServices;

public class ConstructionSitePermissionService(
    IConstructionSiteAccessChecker accessChecker,
    IRepository<ConstructionSite> repository)
    : EntityPermissionServiceBase<ConstructionSite, ConstructionSiteUpdateArgs, ConstructionSitePermission>(
        accessChecker, repository)
{
    protected override async Task FillPermissions(Guid? entityId, ConstructionSitePermission permissions, CancellationToken cancellationToken)
    {
        if (entityId is null)
            return;
        
        var entity = await Repository.GetByIdAsync(entityId.Value, cancellationToken);
        if (entity is null)
            return;
        
        permissions.CanUpdateOrders = await accessChecker.CanUpdateOrders(entity);
    }

    protected override ConstructionSiteUpdateArgs FillUpdateArgs() =>
        new()
        {
            ShortName = string.Empty,
            FullName = string.Empty,
            Address = string.Empty,
            Orders = new ListUpdate<OrderCreationArgs>
            {
                Add = [new OrderCreationArgs{FileId = string.Empty, UserInChargeId = Guid.Empty}],
                Remove = [Guid.Empty]
            },
            UserRoles = new ListUpdate<ConstructionSiteUserRoleCreationArgs, ConstructionSiteUserRoleUpdateArgs>
            {
                Add = [new ConstructionSiteUserRoleCreationArgs{UserId = Guid.Empty, Role = ConstructionSiteUserRoleType.Admin}],
                Remove = [Guid.Empty]
            },
            OrganizationId = Guid.Empty,
            SubOrganizationId = Guid.Empty
        };
}