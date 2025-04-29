using Api.Models.Models.Roles;

namespace Api.Models.Models;

public class ObjectUserRole : EntityInfo
{
    public required Guid ConstructionSiteId { get; set; }
    
    public required Guid UserId { get; set; }
    
    public required ObjectUserRoleType Role { get; set; }
    
    public required DateTime AssignedAt { get; set; }
}