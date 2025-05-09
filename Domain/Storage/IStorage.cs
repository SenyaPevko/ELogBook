using Domain.Entities.Base;
using Domain.RequestArgs.SearchRequest;

namespace Domain.Storage;

public interface IStorage<TEntity>
    where TEntity : EntityInfo, new()
{
    public Task AddAsync(TEntity entity);

    public Task<TEntity?> GetByIdAsync(Guid id);

    public Task UpdateAsync(TEntity entity);

    public Task DeleteAsync(TEntity entity);
}

public interface IStorage<TEntity, in TSearchRequest> : IStorage<TEntity>
    where TEntity : EntityInfo, new()
    where TSearchRequest : SearchRequestBase
{
    public Task<List<TEntity>> SearchAsync(TSearchRequest request);
}