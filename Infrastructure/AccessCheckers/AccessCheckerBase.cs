using Domain.AccessChecker;
using Domain.Entities.Base;
using Domain.Entities.Roles;
using Domain.RequestArgs.Base;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers;

public abstract class AccessCheckerBase<TEntity, TUpdateArgs>(IRequestContext context)
    : AccessCheckerBase<TEntity>(context), IAccessChecker<TEntity, TUpdateArgs>
    where TEntity : EntityInfo
    where TUpdateArgs : IEntityUpdateArgs
{
    public virtual async Task<bool> CanUpdate(TUpdateArgs updateArgs, TEntity oldEntity, TEntity newEntity)
    {
        return true;
    }
}

public abstract class AccessCheckerBase<TEntity>(IRequestContext context) : IAccessChecker<TEntity>
    where TEntity : EntityInfo
{
    protected readonly IRequestContext Context = context;

    public virtual async Task<bool?> CanRead()
    {
        if (Context.Auth.Role is UserRole.Admin)
            return true;

        return null;
    }

    public virtual async Task<bool> CanRead(TEntity entity)
    {
        return true;
    }

    public virtual async Task<bool?> CanUpdate()
    {
        if (Context.Auth.Role is UserRole.Admin)
            return true;

        return null;
    }

    public virtual async Task<bool> CanUpdate(TEntity entity)
    {
        return true;
    }

    public virtual async Task<bool?> CanCreate()
    {
        if (Context.Auth.Role is UserRole.Admin)
            return true;

        return null;
    }

    public virtual async Task<bool> CanCreate(TEntity entity)
    {
        return true;
    }
}