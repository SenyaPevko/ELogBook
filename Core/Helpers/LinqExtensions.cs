namespace Core.Helpers;

public static class LinqExtensions
{
    // todo: есть места где используется foreach заместо selectasync нужно заменить
    public static async Task<TResult[]> SelectAsync<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, Task<TResult>> selector)
    {
        return await Task.WhenAll(source.Select(selector));
    }
}