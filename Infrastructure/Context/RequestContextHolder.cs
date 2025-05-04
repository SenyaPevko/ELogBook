namespace Infrastructure.Context;

public static class RequestContextHolder
{
    private static readonly AsyncLocal<IRequestContext> _current = new();

    public static IRequestContext Current
    {
        get => _current.Value ?? throw new InvalidOperationException("Context not set");
        set => _current.Value = value;
    }
}