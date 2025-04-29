namespace Domain.Dbo;

/// <summary>
/// Организация
/// </summary>
public class OrganizationDbo : UpdatableDomainEntityDbo
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