using Domain.Entities.Base;

namespace Domain.Entities.Organization;

public class Organization : EntityInfo
{
    /// <summary>
    ///     Название организации
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Id пользователей организации
    /// </summary>
    public List<Guid> UserIds { get; set; } = [];
}