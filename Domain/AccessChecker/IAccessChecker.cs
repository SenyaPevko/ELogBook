using Domain.Entities.Base;

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