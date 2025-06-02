using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Permissions;
using Domain.Permissions.ConstructionSite;
using Domain.Repository;
using Infrastructure.Context;
using Infrastructure.Permissions.Base;

namespace Infrastructure.Permissions;

public class PermissionService(
    IEntityPermissionService<ConstructionSitePermission> constructionSitePermissionService, 
    IEntityPermissionService<RegistrationSheetItemPermission> regSheetItemPermissionService,
    IEntityPermissionService<RecordSheetItemPermission> recSheetItemPermissionService,
    IRepository<User> userRepository,
    IRequestContext requestContext) 
    : IPermissionService
{
    public async Task<GlobalPermission> GetUserGlobalPermissionsAsync(CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(requestContext.Auth.UserId!.Value, cancellationToken);
        if (user is null)
            return new GlobalPermission();
        
        var constructionPermission = await GetUserConstructionSitePermissionsAsync(null,null, null, cancellationToken);

        return new GlobalPermission
        {
            CanAccessAdminPanel = user.UserRole is UserRole.Admin,
            ConstructionSitePermission = constructionPermission,
        };
    }

    public async Task<ConstructionSitePermission> GetUserConstructionSitePermissionsAsync(
        Guid? constructionSiteId,
        Guid? recSheetItemId,
        Guid? regSheetItemId,
        CancellationToken cancellationToken)
    {
        var constructionPermission = await constructionSitePermissionService.GetUserPermissions(
            constructionSiteId, cancellationToken);
        var regSheetItemPermission = await regSheetItemPermissionService.GetUserPermissions(
            regSheetItemId,
            cancellationToken);
        var recSheetItemPermission = await recSheetItemPermissionService.GetUserPermissions(
            recSheetItemId,
            cancellationToken);

        return new ConstructionSitePermission
        {
            CanCreate = constructionPermission.CanCreate,
            CanRead = constructionPermission.CanRead,
            CanUpdate = constructionPermission.CanUpdate,
            RecordSheetItemPermission = recSheetItemPermission,
            RegistrationSheetItemPermission = regSheetItemPermission
        };
    }
}