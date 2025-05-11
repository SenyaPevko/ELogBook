using Domain.Entities.Base;
using Domain.Entities.Roles;

namespace Domain.Entities.ConstructionSite;

public class ConstructionSiteUserRole : IItemWithId
{
    public required Guid UserId { get; set; }

    public required ConstructionSiteUserRoleType Role { get; set; }
    public Guid Id { get; set; }
}