using Domain.AccessChecker;
using Domain.Entities.Base;
using Domain.Entities.Roles;
using Infrastructure.Context;

namespace Infrastructure.AccessChecker;

public class AccessCheckerBase<TEntity>(IRequestContext context) : IAccessChecker<TEntity>
    where TEntity : EntityInfo
{
    public Task<bool?> CanRead()
    {
        if (context.Auth.Role is UserRole.Admin)
            return true;
        if (context.Auth.Role is UserRole.Unknown)
            return null;
    }

    public Task<bool> CanRead(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> CanUpdate()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CanUpdate(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> CanCreate()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CanCreate(TEntity entity)
    {
        throw new NotImplementedException();
    }
}