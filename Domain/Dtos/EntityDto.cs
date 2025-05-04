using Domain.Entities.Base;

namespace Domain.Dtos;

public abstract class EntityDto
{
    /// <summary>
    ///     Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }

    public UpdateInfo? UpdateInfo { get; set; }
}