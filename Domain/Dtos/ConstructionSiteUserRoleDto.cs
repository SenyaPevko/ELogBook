using Domain.Entities.Roles;

namespace Domain.Dtos;

public class ConstructionSiteUserRoleDto : EntityDto
{
    public required Guid ConstructionSiteId { get; set; }

    public required Guid UserId { get; set; }

    public required ConstructionSiteUserRoleType Role { get; set; }

    public required DateTime AssignedAt { get; set; }
}