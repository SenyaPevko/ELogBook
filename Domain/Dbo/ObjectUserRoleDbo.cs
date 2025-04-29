using Api.Models.Models.Roles;

namespace Domain.Dbo;

/// <summary>
/// Роль пользователя в рамках объекта
/// </summary>
public class ObjectUserRoleDbo : EntityDbo
{
    public required Guid ObjectId { get; set; }
    
    public required Guid UserId { get; set; }
    
    public required ObjectUserRoleType Role { get; set; }
    
    public required DateTime AssignedAt { get; set; }
}