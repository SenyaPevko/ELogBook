namespace Domain.Dbo;

/// <summary>
/// Организация
/// </summary>
public class OrganizationDbo : EntityDbo
{
    /// <summary>
    /// Название организации
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Id пользователей организации
    /// </summary>
    public List<Guid> UserIds { get; set; } = [];
}