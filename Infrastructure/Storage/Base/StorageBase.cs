using Core.Helpers;
using Domain.Entities.Base;
using Domain.RequestArgs.Base;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo;
using MongoDB.Driver;

namespace Infrastructure.Storage.Base;

public abstract class StorageBase<TEntity, TDbo, TSearchRequest>(IRequestContext requestContext)
    : StorageBase<TEntity, TDbo>(requestContext), IStorage<TEntity, TSearchRequest>
    where TEntity : EntityInfo, new()
    where TDbo : EntityDbo, new()
    where TSearchRequest : SearchRequestBase
{
    private static readonly FilterDefinitionBuilder<TDbo> FilterBuilder = Builders<TDbo>.Filter;
    private static readonly SortDefinitionBuilder<TDbo> SortBuilder = Builders<TDbo>.Sort;

    public async Task<List<TEntity>> SearchAsync(TSearchRequest request)
    {
        if (IsEmptySearchRequest(request) && request.GetAll is not true)
        {
            return new List<TEntity>();
        }

        var filters = BuildBaseFilters(request);

        var specificFilters = BuildSpecificFilters(request);
        if (specificFilters != null) filters.AddRange(specificFilters);

        var combinedFilter = filters.Any()
            ? FilterBuilder.And(filters)
            : FilterBuilder.Empty;

        var sort = BuildSortDefinition(request);
        var (skip, limit) = CalculatePagination(request);

        var cursor = await Collection.FindAsync(combinedFilter, new FindOptions<TDbo>
        {
            Sort = sort,
            Skip = skip,
            Limit = limit
        });

        var dbos = await cursor.ToListAsync();
        return await ConvertToEntitiesAsync(dbos);
    }

    protected virtual bool IsEmptySearchRequest(TSearchRequest request)
    {
        return (request.Ids == null || request.Ids.Count == 0) &&
               string.IsNullOrEmpty(request.SortBy) &&
               !request.Page.HasValue &&
               !request.PageSize.HasValue &&
               IsSpecificSearchRequestEmpty(request);
    }

    protected virtual bool IsSpecificSearchRequestEmpty(TSearchRequest request) => true;

    protected virtual List<FilterDefinition<TDbo>> BuildBaseFilters(TSearchRequest request)
    {
        var filters = new List<FilterDefinition<TDbo>>();

        if (request.Ids?.Count > 0) filters.Add(FilterBuilder.In(x => x.Id, request.Ids));

        return filters;
    }

    protected virtual List<FilterDefinition<TDbo>>? BuildSpecificFilters(TSearchRequest request)
    {
        return null;
    }

    protected SortDefinition<TDbo>? BuildSortDefinition(SearchRequestBase request)
    {
        return !string.IsNullOrEmpty(request.SortBy)
            ? request.SortDescending
                ? SortBuilder.Descending(request.SortBy)
                : SortBuilder.Ascending(request.SortBy)
            : null;
    }

    protected (int? skip, int? limit) CalculatePagination(SearchRequestBase request)
    {
        return request.Page.HasValue && request.PageSize.HasValue
            ? ((request.Page.Value - 1) * request.PageSize.Value, request.PageSize.Value)
            : (null, null);
    }

    protected async Task<List<TEntity>> ConvertToEntitiesAsync(List<TDbo> dbos)
    {
        var entities = new List<TEntity>();
        foreach (var dbo in dbos) entities.Add(await ToEntityAsync(dbo));
        return entities;
    }
}

public abstract class StorageBase<TEntity, TDbo>(IRequestContext requestContext)
    : IStorage<TEntity>
    where TEntity : EntityInfo, new()
    where TDbo : EntityDbo, new()
{
    protected abstract IMongoCollection<TDbo> Collection { get; }

    public virtual async Task AddAsync(TEntity entity)
    {
        var dbo = await CreateDbo(entity);

        await Collection.InsertOneAsync(dbo);
        DboHelper.UpdateEntityInfo(entity, dbo);
        await MapEntityFromDboAsync(entity, dbo);
    }

    public virtual async Task AddManyAsync(List<TEntity> entities)
    {
        var dbos = await entities.SelectAsync(CreateDbo);
        var idToEntity = entities.ToDictionary(e => e.Id);

        await Collection.InsertManyAsync(dbos);

        foreach (var dbo in dbos)
        {
            DboHelper.UpdateEntityInfo(idToEntity[dbo.Id], dbo);
            await MapEntityFromDboAsync(idToEntity[dbo.Id], dbo);
        }
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        var dbo = await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return dbo == null ? null : await ToEntityAsync(dbo);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var existingDbo = await Collection.Find(x => x.Id == entity.Id).FirstOrDefaultAsync();
        var existingEntity = await GetByIdAsync(entity.Id);

        DboHelper.UpdateEntityDbo(existingDbo!, requestContext);
        await MapDboFromEntityAsync(existingEntity, entity, existingDbo!);

        await Collection.ReplaceOneAsync(x => x.Id == entity.Id, existingDbo!);
        DboHelper.UpdateEntityInfo(entity, existingDbo!);
        await MapEntityFromDboAsync(entity, existingDbo!);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await Collection.DeleteOneAsync(x => x.Id == entity.Id);
    }

    private async Task<TDbo> CreateDbo(TEntity entity)
    {
        var dbo = DboHelper.CreateEntityDbo<TDbo>(requestContext);
        dbo.Id = entity.Id;
        await MapDboFromEntityAsync(entity, dbo);

        return dbo;
    }

    protected async Task<TEntity> ToEntityAsync(TDbo dbo)
    {
        var result = new TEntity
        {
            Id = dbo.Id,
            UpdateInfo = dbo.ToUpdateInfo()
        };

        await MapEntityFromDboAsync(result, dbo);
        return result;
    }

    protected abstract Task MapEntityFromDboAsync(TEntity entity, TDbo dbo);
    protected abstract Task MapDboFromEntityAsync(TEntity entity, TDbo dbo);
    protected abstract Task MapDboFromEntityAsync(TEntity? existingEntity, TEntity newEntity, TDbo dbo);
}