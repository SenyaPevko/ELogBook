using System.Linq.Expressions;
using System.Reflection;

namespace Utils;

public static class ObjectCloner
{
    private static readonly Dictionary<Type, Delegate> CloneCache = new();

    public static T Copy<T>(this T source) where T : class, new()
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var type = typeof(T);

        if (!CloneCache.TryGetValue(type, out var copier))
        {
            copier = CreateCopyFunc<T>();
            CloneCache[type] = copier;
        }

        return ((Func<T, T>)copier)(source);
    }

    private static Func<T, T> CreateCopyFunc<T>() where T : class, new()
    {
        var sourceParam = Expression.Parameter(typeof(T), "source");
        var bindings = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite)
            .Select(prop =>
                Expression.Bind(prop, Expression.Property(sourceParam, prop)));

        var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
        var lambda = Expression.Lambda<Func<T, T>>(body, sourceParam);
        return lambda.Compile();
    }
}