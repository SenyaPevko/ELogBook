namespace Domain.RequestArgs.Base;

public abstract class EntityCreationArgs : IEntityCreationArgs
{
    public Guid Id { get; set; }
}