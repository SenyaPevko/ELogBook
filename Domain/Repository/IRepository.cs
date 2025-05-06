using Domain.Entities.Base;
using Domain.RequestArgs.SearchRequest;

namespace Domain.Repository;

public interface IRepository<TEntity>
    where TEntity : EntityInfo, new()
{
    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

    public Task<List<TEntity>> SearchAsync(SearchRequest request, CancellationToken cancellationToken);
}

public interface IRepository<TEntity, TInvalidReason> : IRepository<TEntity>
    where TEntity : EntityInfo, new()
    where TInvalidReason : Enum
{
    public Task AddAsync(TEntity entity, IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken);

    public Task UpdateAsync(TEntity entity, IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken);
}