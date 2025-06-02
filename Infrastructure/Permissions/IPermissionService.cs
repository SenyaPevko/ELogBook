using Domain.Permissions;
using Domain.Permissions.ConstructionSite;

namespace Infrastructure.Permissions;

public interface IPermissionService
{
    Task<GlobalPermission> GetUserGlobalPermissionsAsync(CancellationToken cancellationToken);
    Task<ConstructionSitePermission> GetUserConstructionSitePermissionsAsync(
        Guid? constructionSiteId,
        Guid? recSheetItemId,
        Guid? regSheetItemId,
        CancellationToken cancellationToken);
}