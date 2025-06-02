using Domain.AccessChecker;
using Domain.Entities.ConstructionSite;
using Domain.Permissions.ConstructionSite;
using Domain.Repository;
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
}