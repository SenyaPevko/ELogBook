using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Roles;

namespace Infrastructure.Dbo;

/// <summary>
///     Роль пользователя в рамках объекта
/// </summary>
[Table("constructionSiteUserRoles")]
public class ConstructionSiteUserRoleDbo : EntityDbo
{
    public required Guid ConstructionSiteId { get; set; }

    public required Guid UserId { get; set; }

    public required ConstructionSiteUserRoleType Role { get; set; }

    public required DateTime AssignedAt { get; set; }
}