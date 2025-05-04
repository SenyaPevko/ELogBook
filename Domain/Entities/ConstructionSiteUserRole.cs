using Domain.Entities.Base;
using Domain.Entities.Roles;

namespace Domain.Entities;

public class ConstructionSiteUserRole : EntityInfo
{
    public required Guid ConstructionSiteId { get; set; }

    public required Guid UserId { get; set; }

    public required ConstructionSiteUserRoleType Role { get; set; }

    public required DateTime AssignedAt { get; set; }
}