using Domain.Permissions.ConstructionSite;

namespace Domain.Permissions;

public class GlobalPermission
{
    public bool CanAccessAdminPanel { get; set; }
    public ConstructionSitePermission ConstructionSitePermission { get; set; } = null!;
    public OrganizationPermission OrganizationPermission { get; set; } = null!;
    public UserPermission UserPermission { get; set; } = null!;
}