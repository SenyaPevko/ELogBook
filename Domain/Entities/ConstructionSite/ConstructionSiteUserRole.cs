using Domain.Entities.Base;
using Domain.Entities.Roles;

namespace Domain.Entities.ConstructionSite;

public class ConstructionSiteUserRole : EntityInfo
{
    public required Guid UserId { get; set; }

    public required ConstructionSiteUserRoleType Role { get; set; }
}