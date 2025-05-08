using System.Linq.Expressions;
using Domain.Models.Filters;
using Domain.RequestArgs.SearchRequest;

namespace Infrastructure.Helpers.SearchRequestHelper;

public static class SearchRequestExtensions
{
    public static SearchRequest WhereEquals<TEntity, TProperty>(
        this SearchRequest request,
        Expression<Func<TEntity, TProperty>> selector,
        TProperty value)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(selector);

        var member = selector.Body switch
        {
            MemberExpression m => m,
            UnaryExpression { Operand: MemberExpression m2 } => m2,
            _ => throw new ArgumentException("Selector must be a simple property access", nameof(selector))
        };
        var fieldName = member.Member.Name;

        request.Filters.Add(new Filter
        {
            Field = fieldName,
            Operator = FilterOperator.Equals,
            Value = value!
        });

        return request;
    }
    
    public static SearchRequest WhereIn<TEntity, TProperty>(
        this SearchRequest request,
        Expression<Func<TEntity, TProperty>> selector,
        IEnumerable<TProperty> values)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(selector);
        ArgumentNullException.ThrowIfNull(values);

        var member = selector.Body switch
        {
            MemberExpression m => m,
            UnaryExpression { Operand: MemberExpression m2 } => m2,
            _ => throw new ArgumentException("Selector must be a simple property access", nameof(selector))
        };
        var fieldName = member.Member.Name;

        request.Filters.Add(new Filter
        {
            Field = fieldName,
            Operator = FilterOperator.In,
            Value = values.ToList()
        });

        return request;
    }

    public static SearchRequest SinglePage(this SearchRequest request)
    {
        request.Page = 1;
        request.PageSize = 1;
        return request;
    }
}