using Domain.AccessChecker;
using Domain.Entities.Base;
using Domain.Permissions.Base;
using Domain.Repository;
using Domain.RequestArgs.Base;

namespace Infrastructure.Permissions.Base;

public abstract class EntityPermissionServiceBase<TEntity, TUpdateArgs, TPermission>(
    IAccessChecker<TEntity, TUpdateArgs> accessChecker,
    IRepository<TEntity> repository) 
    : IEntityPermissionService<TPermission>
    where TPermission : EntityPermissionsBase, new()
    where TEntity : EntityInfo, new()
    where TUpdateArgs : IEntityUpdateArgs
{
    protected readonly IAccessChecker<TEntity, TUpdateArgs> AccessChecker = accessChecker;
    protected readonly IRepository<TEntity> Repository = repository;
    
    public async Task<TPermission> GetUserPermissions(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = entityId is null ? null : await Repository.GetByIdAsync(entityId.Value, cancellationToken);

        var permission = new TPermission
        {
            CanCreate = await CanCreate(entity, cancellationToken),
            CanUpdate = await CanUpdate(entity, cancellationToken),
            CanRead = await CanRead(entity, cancellationToken),
        };
        await FillPermissions(entityId, permission, cancellationToken);
        
        return permission;
    }

    protected virtual async Task FillPermissions(Guid? entityId, TPermission permissions, CancellationToken cancellationToken)
    {
    }
    
    protected virtual async Task<bool> CanCreate(TEntity? entity, CancellationToken cancellationToken)
    {
        var canCreate = await AccessChecker.CanCreate();
        if (entity is not null)
            canCreate ??= await AccessChecker.CanCreate(entity);
        
        return canCreate ?? false;
    }
    
    protected virtual async Task<bool> CanUpdate(TEntity? entity, CancellationToken cancellationToken)
    {
        var canUpdate = await AccessChecker.CanUpdate();
        if (entity is not null)
            canUpdate ??= await AccessChecker.CanUpdate(entity) && await AccessChecker.CanUpdate(FillUpdateArgs(), entity, entity);
        
        return canUpdate ?? false;
    }
    
    protected virtual async Task<bool> CanRead(TEntity? entity, CancellationToken cancellationToken)
    {
        var canRead = await AccessChecker.CanRead();
        if (entity is not null)
            canRead ??= await AccessChecker.CanRead(entity);
        
        return canRead ?? false;
    }

    protected abstract TUpdateArgs FillUpdateArgs();
}