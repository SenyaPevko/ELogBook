using Domain.Entities.Roles;
using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

public class ConstructionSiteUserRoleCreationArgs : EntityCreationArgs
{
    public required Guid UserId { get; set; }

    public required ConstructionSiteUserRoleType Role { get; set; }
}