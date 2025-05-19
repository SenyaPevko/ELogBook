using Domain;
using Domain.Entities.Base;
using Domain.Repository;
using Domain.RequestArgs.Base;
using Domain.Storage;

namespace Infrastructure.Repository;

public abstract class RepositoryBase<TEntity, TInvalidReason, TSearchRequest>(IStorage<TEntity, TSearchRequest> storage)
    : RepositoryBase<TEntity, TInvalidReason>(storage), IRepository<TEntity, TInvalidReason, TSearchRequest>
    where TEntity : EntityInfo, new()
    where TInvalidReason : Enum
    where TSearchRequest : SearchRequestBase
{
    public async Task<List<TEntity>> SearchAsync(TSearchRequest request, CancellationToken cancellationToken)
    {
        // todo: мб валидация запроса поиска
        return await storage.SearchAsync(request);
    }
}

public abstract class RepositoryBase<TEntity, TInvalidReason>(IStorage<TEntity> storage)
    : IRepository<TEntity, TInvalidReason>
    where TEntity : EntityInfo, new()
    where TInvalidReason : Enum
{
    public virtual async Task AddAsync(TEntity entity, IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken)
    {
        await PreprocessCreationAsync(entity, writeContext, cancellationToken);
        if (!writeContext.IsSuccess)
            return;

        await ValidateCreationAsync(entity, writeContext, cancellationToken);
        if (!writeContext.IsSuccess)
            return;

        await storage.AddAsync(entity);

        await AfterCreateAsync(entity, writeContext, cancellationToken);
    }
    
    public virtual async Task AddManyAsync(List<TEntity> entities, IBulkWriteContext<TEntity, TInvalidReason> writeContext,
        CancellationToken cancellationToken)
    {
        await PreprocessBulkCreationAsync(entities, writeContext, cancellationToken);
        if (!writeContext.IsSuccess)
            return;

        await ValidateBulkCreationAsync(entities, writeContext, cancellationToken);
        if (!writeContext.IsSuccess)
            return;

        await storage.AddManyAsync(entities);

        await AfterBulkCreateAsync(entities, writeContext, cancellationToken);
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
        await ValidateUpdateAsync(existingEntity!, entity, writeContext, cancellationToken);
        if (!writeContext.IsSuccess)
            return;

        await storage.UpdateAsync(entity);
        await AfterUpdateAsync(existingEntity, entity, writeContext, cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await storage.DeleteAsync(entity);
    }

    protected abstract Task ValidateCreationAsync(TEntity entity, IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken);

    protected virtual async Task ValidateBulkCreationAsync(
        List<TEntity> entities,
        IBulkWriteContext<TEntity, TInvalidReason> writeContext,
        CancellationToken cancellationToken)
    {
    }

    protected abstract Task ValidateUpdateAsync(TEntity oldEntity, TEntity newEntity,
        IWriteContext<TInvalidReason> writeContext, CancellationToken cancellationToken);

    protected virtual async Task PreprocessCreationAsync(
        TEntity entity,
        IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken)
    {
    }

    protected virtual async Task PreprocessBulkCreationAsync(
        List<TEntity> entities,
        IBulkWriteContext<TEntity, TInvalidReason> writeContext,
        CancellationToken cancellationToken)
    {
    }

    protected virtual async Task AfterCreateAsync(
        TEntity entity,
        IWriteContext<TInvalidReason> writeContext,
        CancellationToken cancellationToken)
    {
    }
    
    protected virtual async Task AfterBulkCreateAsync(
        List<TEntity> entities,
        IBulkWriteContext<TEntity, TInvalidReason> writeContext,
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