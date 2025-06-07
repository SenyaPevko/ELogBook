namespace Domain.RequestArgs.Base;

public class ListUpdate<TCreationArgs, TUpdateArgs> : ListUpdate<TCreationArgs>
    where TCreationArgs : IEntityCreationArgs
    where TUpdateArgs : IEntityUpdateArgs
{
    public List<TUpdateArgs>? Update { get; set; }
}

public class ListUpdate<TCreationArgs>
    where TCreationArgs : IEntityCreationArgs
{
    public List<TCreationArgs>? Add { get; set; }
    public HashSet<Guid>? Remove { get; set; }
}

public class ListUpdateOfId<T>
{
    public List<T>? Add { get; set; }
    public HashSet<T>? Remove { get; set; }
}