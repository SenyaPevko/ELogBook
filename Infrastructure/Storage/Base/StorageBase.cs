using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Domain.Entities.Base;
using Domain.Models.Filters;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Base;

public abstract class StorageBase<TEntity, TDbo>(AppDbContext context, IRequestContext requestContext)
    : IStorage<TEntity>
    where TEntity : EntityInfo, new()
    where TDbo : EntityDbo, new()
{
    private readonly DbSet<TDbo> _dbSet = context.Set<TDbo>();

    public virtual async Task AddAsync(TEntity entity)
    {
        var dbo = DboHelper.CreateEntityDbo<TDbo>(requestContext);
        dbo.Id = entity.Id;
        await MapDboFromEntityAsync(entity, dbo);

        await _dbSet.AddAsync(dbo);
        await context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        var dbo = await _dbSet.FindAsync(id);

        return dbo is null ? null : await ToEntityAsync(dbo);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var existingDbo = await _dbSet.FindAsync(entity.Id);
        var existingEntity = await GetByIdAsync(entity.Id);
        DboHelper.UpdateEntityDbo(existingDbo!, requestContext);
        await MapDboFromEntityAsync(existingEntity, entity, existingDbo!);

        // todo: на фронт возвращается старое updateInfo, тк оно записывается в базу, а возвращается старое updateInfo
        _dbSet.Update(existingDbo!);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var dbo = await _dbSet.FindAsync(entity.Id);

        _dbSet.Remove(dbo!);
        await context.SaveChangesAsync();
    }

    public async Task<List<TEntity>> SearchAsync(SearchRequest request)
    {
        var query = _dbSet.AsQueryable();

        query = (from f in request.Filters
                let param = Expression.Parameter(typeof(TDbo), "x")
                let member = Expression.PropertyOrField(param, f.Field)
                let constant = Expression.Constant(Convert.ChangeType(f.Value, member.Type))
                let predicateBody = (Expression)(f.Operator switch
                {
                    FilterOperator.Equals => Expression.Equal(member, constant),
                    FilterOperator.GreaterThan => Expression.GreaterThan(member, constant),
                    FilterOperator.LessThan => Expression.LessThan(member, constant),
                    FilterOperator.Contains => Expression.Call(member, nameof(string.Contains), Type.EmptyTypes,
                        Expression.Constant(f.Value?.ToString())),
                    _ => throw new NotSupportedException($"Operator {f.Operator} not supported")
                })
                select Expression.Lambda<Func<TDbo, bool>>(predicateBody!, param))
            .Aggregate(query, (current, lambda) => current.Where(lambda));

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