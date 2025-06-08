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
    IEntityPermissionService<WorkIssueItemPermission> workIssueItemPermissionService,
    IEntityPermissionService<OrganizationPermission> organizationPermissionService,
    IEntityPermissionService<UserPermission> userPermissionService,
    IRepository<User> userRepository,
    IRequestContext requestContext) 
    : IPermissionService
{
    public async Task<GlobalPermission> GetUserGlobalPermissionsAsync(CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(requestContext.Auth.UserId!.Value, cancellationToken);
        if (user is null)
            return new GlobalPermission();
        
        var constructionPermission = await GetUserConstructionSitePermissionsAsync(null, cancellationToken);
        var organizationPermission = await GetUserOrganizationPermissionsAsync(null, cancellationToken);
        var userPermission = await GetUserPermissionsAsync(null, cancellationToken);

        return new GlobalPermission
        {
            CanAccessAdminPanel = user.UserRole is UserRole.Admin,
            ConstructionSitePermission = constructionPermission,
            OrganizationPermission = organizationPermission,
            UserPermission = userPermission,
        };
    }

    public async Task<OrganizationPermission> GetUserOrganizationPermissionsAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        return await organizationPermissionService.GetUserPermissions(entityId, cancellationToken);
    }
    
    public async Task<UserPermission> GetUserPermissionsAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        return await userPermissionService.GetUserPermissions(entityId, cancellationToken);
    }

    public async Task<ConstructionSitePermission> GetUserConstructionSitePermissionsAsync(
        Guid? constructionSiteId,
        CancellationToken cancellationToken)
    {
        var constructionPermission = await constructionSitePermissionService.GetUserPermissions(
            constructionSiteId, cancellationToken);
        var regSheetItemPermission = await regSheetItemPermissionService.GetUserPermissions(
            constructionSiteId,
            cancellationToken);
        var recSheetItemPermission = await recSheetItemPermissionService.GetUserPermissions(
            constructionSiteId,
            cancellationToken);
        var workIssueItemPermission = await workIssueItemPermissionService.GetUserPermissions(
            constructionSiteId,
            cancellationToken);

        return new ConstructionSitePermission
        {
            CanCreate = constructionPermission.CanCreate,
            CanRead = constructionPermission.CanRead,
            CanUpdate = constructionPermission.CanUpdate,
            RecordSheetItemPermission = recSheetItemPermission,
            RegistrationSheetItemPermission = regSheetItemPermission,
            WorkIssueItemPermission = workIssueItemPermission
        };
    }
}