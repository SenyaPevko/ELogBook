using Domain.Entities.Base;
using Domain.RequestArgs.Base;

namespace Domain.AccessChecker;


public interface IAccessChecker<in TEntity> 
    where TEntity : EntityInfo
{
    Task<bool?> CanRead();
    Task<bool> CanRead(TEntity entity);

    Task<bool?> CanUpdate();
    Task<bool> CanUpdate(TEntity entity);

    Task<bool?> CanCreate();
    Task<bool> CanCreate(TEntity entity);
}

public interface IAccessChecker<in TEntity, in TUpdateArgs> : IAccessChecker<TEntity> 
    where TEntity : EntityInfo
    where TUpdateArgs : IEntityUpdateArgs
{
    Task<bool> CanUpdate(TUpdateArgs updateArgs, TEntity oldEntity, TEntity newEntity);
}