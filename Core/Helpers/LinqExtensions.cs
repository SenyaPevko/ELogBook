namespace Core.Helpers;

public static class LinqExtensions
{
    public static async Task<TResult[]> SelectAsync<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, Task<TResult>> selector)
    {
        return await Task.WhenAll(source.Select(selector));
    }
    
    public static async Task SelectAsync<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, Task> selector)
    {
        await Task.WhenAll(source.Select(selector));
    }
}