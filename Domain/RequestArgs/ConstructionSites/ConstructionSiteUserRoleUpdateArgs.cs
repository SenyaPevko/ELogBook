using Domain.Entities.Roles;
using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

public class ConstructionSiteUserRoleUpdateArgs : EntityUpdateArgs
{
    public required ConstructionSiteUserRoleType Role { get; set; }
}