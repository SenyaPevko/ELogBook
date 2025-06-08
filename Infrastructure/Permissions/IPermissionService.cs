using Domain.Permissions;
using Domain.Permissions.ConstructionSite;

namespace Infrastructure.Permissions;

public interface IPermissionService
{
    Task<GlobalPermission> GetUserGlobalPermissionsAsync(CancellationToken cancellationToken);
    Task<OrganizationPermission> GetUserOrganizationPermissionsAsync(Guid? entityId, CancellationToken cancellationToken);
    Task<UserPermission> GetUserPermissionsAsync(Guid? entityId, CancellationToken cancellationToken);
    Task<ConstructionSitePermission> GetUserConstructionSitePermissionsAsync(
        Guid? constructionSiteId,
        CancellationToken cancellationToken);
}