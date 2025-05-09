using Core.Helpers;
using Domain.Entities.Base;
using Domain.Models.Filters;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Storage.Base;

public abstract class StorageBase<TEntity, TDbo>(AppDbContext context, IRequestContext requestContext)
    : IStorage<TEntity>
    where TEntity : EntityInfo, new()
    where TDbo : EntityDbo, new()
{
    private static readonly FilterDefinitionBuilder<TDbo> FilterBuilder = Builders<TDbo>.Filter;
    private static readonly SortDefinitionBuilder<TDbo> SortBuilder = Builders<TDbo>.Sort;
    
    protected abstract IMongoCollection<TDbo> Collection { get; }

    public virtual async Task AddAsync(TEntity entity)
    {
        var dbo = DboHelper.CreateEntityDbo<TDbo>(requestContext);
        dbo.Id = entity.Id;
        await MapDboFromEntityAsync(entity, dbo);

        await Collection.InsertOneAsync(dbo);
        DboHelper.UpdateEntityInfo(entity, dbo);
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
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await Collection.DeleteOneAsync(x => x.Id == entity.Id);
    }

    public async Task<List<TEntity>> SearchAsync(SearchRequest request)
    {
        var filterDefinitions = new List<FilterDefinition<TDbo>>();

        foreach (var filter in request.Filters)
        {
            FilterDefinition<TDbo> filterDefinition = filter.Operator switch
            {
                FilterOperator.Equals =>
                    FilterBuilder.Eq(filter.Field, filter.Value),

                FilterOperator.GreaterThan =>
                    FilterBuilder.Gt(filter.Field, filter.Value),

                FilterOperator.LessThan =>
                    FilterBuilder.Lt(filter.Field, filter.Value),

                FilterOperator.Contains when filter.Value is string stringValue =>
                    FilterBuilder.Regex(filter.Field, new BsonRegularExpression(stringValue, "i")),

                FilterOperator.In when filter.Value is System.Collections.IEnumerable enumerable =>
                    FilterBuilder.In(filter.Field, enumerable.Cast<object>()),

                _ => throw new NotSupportedException($"Operator {filter.Operator} not supported")
            };

            filterDefinitions.Add(filterDefinition);
        }

        var combinedFilter = filterDefinitions.Any()
            ? FilterBuilder.And(filterDefinitions)
            : FilterBuilder.Empty;

        var findOptions = new FindOptions<TDbo>
        {
            Sort = !string.IsNullOrEmpty(request.SortBy)
                ? request.SortDescending
                    ? SortBuilder.Descending(request.SortBy)
                    : SortBuilder.Ascending(request.SortBy)
                : null,
            Skip = request.Page.HasValue && request.PageSize.HasValue
                ? (request.Page.Value - 1) * request.PageSize.Value
                : (int?)null,
            Limit = request.PageSize
        };

        var cursor = await Collection.FindAsync(combinedFilter, findOptions);
        var dbos = await cursor.ToListAsync();
        var entities = await dbos.SelectAsync(ToEntityAsync);

        return entities.ToList();
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