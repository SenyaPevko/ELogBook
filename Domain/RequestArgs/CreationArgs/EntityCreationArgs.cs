namespace Domain.RequestArgs.CreationArgs;

public abstract class EntityCreationArgs : IEntityCreationArgs
{
    public Guid Id { get; set; }
}