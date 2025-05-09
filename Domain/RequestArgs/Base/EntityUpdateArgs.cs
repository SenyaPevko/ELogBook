namespace Domain.RequestArgs.Base;

public class EntityUpdateArgs : IEntityUpdateArgs
{
    public Guid Id { get; set; }
}