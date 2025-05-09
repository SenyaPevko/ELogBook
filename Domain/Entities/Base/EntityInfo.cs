namespace Domain.Entities.Base;

public abstract class EntityInfo : IItemWithId
{
    /// <summary>
    ///     Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }

    public UpdateInfo UpdateInfo { get; set; } = null!;
}