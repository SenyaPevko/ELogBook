using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Dbo;

/// <summary>
///     Организация
/// </summary>
[Table("organizations")]
public class OrganizationDbo : EntityDbo
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