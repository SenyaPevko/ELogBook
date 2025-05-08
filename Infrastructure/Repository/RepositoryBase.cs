using Domain;
using Domain.Entities.Base;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;

namespace Infrastructure.Repository;

public abstract class RepositoryBase<TEntity, TInvalidReason>(IStorage<TEntity> storage)
    : IRepository<TEntity, TInvalidReason>
    where TEntity : EntityInfo, new()
    where TInvalidReason : Enum
{
    public virtual async Task AddAsync(TEntity entity, IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken)
    {
        await ValidateCreationAsync(entity, writeContext, cancellationToken);
        if (!writeContext.IsSuccess)
            return;

        await storage.AddAsync(entity);

        await AfterCreateAsync(entity, writeContext, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // todo: нужен метод RemoveSensitiveData
        return await storage.GetByIdAsync(id);
    }

    public async Task UpdateAsync(TEntity entity, IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken)
    {
        var existingEntity = await storage.GetByIdAsync(entity.Id);
        // todo: валидация
        await storage.UpdateAsync(entity);
        await AfterUpdateAsync(existingEntity, entity, writeContext, cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await storage.DeleteAsync(entity);
    }

    public async Task<List<TEntity>> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        // todo: валидация
        return await storage.SearchAsync(request);
    }

    protected abstract Task ValidateCreationAsync(TEntity entity, IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken);
    
    protected virtual async Task AfterCreateAsync(
        TEntity entity,
        IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken)
    {
    }

    protected virtual async Task AfterUpdateAsync(
        TEntity? oldEntity,
        TEntity newEntity,
        IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken)
    {
    }
}