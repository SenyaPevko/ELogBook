using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Domain.Entities.Base;
using Domain.Models.Filters;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Storage.Base;

public abstract class StorageBase<TEntity, TDbo>(AppDbContext context, IRequestContext requestContext)
    : IStorage<TEntity>
    where TEntity : EntityInfo, new()
    where TDbo : EntityDbo, new()
{
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
        var query = Collection.AsQueryable();

        foreach (var filter in request.Filters)
        {
            var param = Expression.Parameter(typeof(TDbo), "x");
            var member = Expression.PropertyOrField(param, filter.Field);
            var constant = Expression.Constant(Convert.ChangeType(filter.Value, member.Type));

            Expression predicateBody = filter.Operator switch
            {
                FilterOperator.Equals => Expression.Equal(member, constant),
                FilterOperator.GreaterThan => Expression.GreaterThan(member, constant),
                FilterOperator.LessThan => Expression.LessThan(member, constant),
                FilterOperator.Contains => Expression.Call(member,
                    nameof(string.Contains),
                    Type.EmptyTypes,
                    Expression.Constant(filter.Value?.ToString())),
                _ => throw new NotSupportedException($"Operator {filter.Operator} not supported")
            };

            var lambda = Expression.Lambda<Func<TDbo, bool>>(predicateBody, param);
            query = query.Where(lambda);
        }

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var ordering = request.SortBy + (request.SortDescending ? " descending" : "");
            query = query.OrderBy(ordering);
        }

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        var dbos = await query.ToListAsync();
        var result = new List<TEntity>(dbos.Count);

        foreach (var dbo in dbos)
            result.Add(await ToEntityAsync(dbo));

        return result;
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