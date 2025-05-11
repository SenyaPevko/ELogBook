namespace Domain.Entities.Base;

public abstract class EntityInfo : IItemWithId
{
    public UpdateInfo UpdateInfo { get; set; } = null!;

    /// <summary>
    ///     Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }
}