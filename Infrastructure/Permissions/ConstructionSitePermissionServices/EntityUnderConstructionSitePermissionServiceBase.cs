using Domain.AccessChecker;
using Domain.Entities.Base;
using Domain.Permissions.Base;
using Domain.RequestArgs.Base;
using Infrastructure.Permissions.Base;

namespace Infrastructure.Permissions.ConstructionSitePermissionServices;

public abstract class EntityUnderConstructionSitePermissionServiceBase<TEntity, TUpdateArgs, TPermission>(
    IAccessChecker<TEntity, TUpdateArgs> accessChecker,
    IEntityUnderConstructionSiteAccessChecker<TUpdateArgs> accessCheckerUnderConstructionSite) 
    : IEntityPermissionService<TPermission>
    where TPermission : EntityPermissionsBase, new()
    where TEntity : EntityInfo, new()
    where TUpdateArgs : IEntityUpdateArgs
{
    public async Task<TPermission> GetUserPermissions(Guid? constructionSiteId,
        CancellationToken cancellationToken)
    {
        var permission = new TPermission
        {
            CanCreate = await CanCreate(constructionSiteId),
            CanUpdate = await CanUpdate(constructionSiteId),
            CanRead = await CanRead(constructionSiteId),
        };
        await FillPermissions(constructionSiteId, permission);

        return permission;
    }

    protected virtual async Task<bool> CanCreate(Guid? constructionSiteId)
    {
        var canCreate = await accessChecker.CanCreate();
        if (canCreate is not true && constructionSiteId is not null)
        {
            var userRoles = await accessCheckerUnderConstructionSite.GetUserRoleTypes(constructionSiteId.Value);
            canCreate ??= accessCheckerUnderConstructionSite.CanCreate(userRoles);
        }


        return canCreate ?? false;
    }

    protected virtual async Task<bool> CanRead(Guid? constructionSiteId)
    {
        var canRead = await accessChecker.CanRead();
        if (canRead is not true && constructionSiteId is not null)
        {
            var userRoles = await accessCheckerUnderConstructionSite.GetUserRoleTypes(constructionSiteId.Value);
            canRead ??= accessCheckerUnderConstructionSite.CanRead(userRoles);
        }


        return canRead ?? false;
    }

    protected virtual async Task<bool> CanUpdate(Guid? constructionSiteId)
    {
        var canUpdate = await accessChecker.CanUpdate();
        if (constructionSiteId is not null)
        {
            var userRoles = await accessCheckerUnderConstructionSite.GetUserRoleTypes(constructionSiteId.Value);
            canUpdate ??= accessCheckerUnderConstructionSite.CanUpdate(userRoles) && 
                          accessCheckerUnderConstructionSite.CanUpdate(FillUpdateArgs(), userRoles);
        }


        return canUpdate ?? false;
    }
    
    protected virtual async Task FillPermissions(Guid? constructionSiteId, TPermission permissions)
    {
    }
    
    protected abstract TUpdateArgs FillUpdateArgs();
}