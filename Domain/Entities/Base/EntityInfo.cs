namespace Domain.Entities.Base;

public abstract class EntityInfo
{
    /// <summary>
    ///     Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }

    public UpdateInfo UpdateInfo { get; set; } = null!;
}