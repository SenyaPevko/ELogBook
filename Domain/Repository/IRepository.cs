using Domain.Entities.Base;
using Domain.RequestArgs.Base;

namespace Domain.Repository;

public interface IRepository<TEntity>
    where TEntity : EntityInfo, new()
{
    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}

public interface IRepository<TEntity, TInvalidReason> : IRepository<TEntity>
    where TEntity : EntityInfo, new()
    where TInvalidReason : Enum
{
    public Task AddAsync(TEntity entity, IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken);

    public Task UpdateAsync(TEntity entity, IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken);

    Task AddManyAsync(List<TEntity> entities, IBulkWriteContext<TEntity, TInvalidReason> writeContext,
        CancellationToken cancellationToken);
}

public interface IRepository<TEntity, TInvalidReason, in TSearchRequest> : IRepository<TEntity, TInvalidReason>
    where TEntity : EntityInfo, new()
    where TInvalidReason : Enum
    where TSearchRequest : SearchRequestBase
{
    public Task<List<TEntity>> SearchAsync(TSearchRequest request, CancellationToken cancellationToken);
}