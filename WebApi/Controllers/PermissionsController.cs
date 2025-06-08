using Domain.Permissions;
using Domain.Permissions.ConstructionSite;
using Infrastructure.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELogBook.Controllers;

[ApiController]
[Route("api/" + "[controller]")]
[Authorize]
public class PermissionsController : ControllerBase
{
    /// <summary>
    /// Разрешения в рамках сайта
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    [HttpGet("global")]
    public async Task<ActionResult<GlobalPermission>> GetUserGlobalPermissionsAsync([FromServices] IPermissionService service)
    {
        return await service.GetUserGlobalPermissionsAsync(HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Разрешения в рамках строительного объекта
    /// </summary>
    /// <param name="service"></param>
    /// <param name="constructionSiteId"></param>
    /// <returns></returns>
    [HttpGet("constructionSite/{constructionSiteId:guid}")]
    public async Task<ActionResult<ConstructionSitePermission>> GetUserConstructionSitePermissionsAsync(
        [FromServices] IPermissionService service,
        [FromRoute] Guid? constructionSiteId)
    {
        return await service.GetUserConstructionSitePermissionsAsync(
            constructionSiteId,
            HttpContext.RequestAborted);
    }

    /// <summary>
    /// Разрешения в рамках организации
    /// </summary>
    /// <param name="service"></param>
    /// <param name="organizationId"></param>
    /// <returns></returns>
    [HttpGet("organization/{organizationId:guid}")]
    public async Task<ActionResult<OrganizationPermission>> GetUserOrganizationPermissionsAsync(
        [FromServices] IPermissionService service,
        [FromRoute] Guid? organizationId)
    {
        return await service.GetUserOrganizationPermissionsAsync(organizationId, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Разрешения в рамках организации
    /// </summary>
    /// <param name="service"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<UserPermission>> GetUserPermissionsAsync(
        [FromServices] IPermissionService service,
        [FromRoute] Guid? userId)
    {
        return await service.GetUserPermissionsAsync(userId, HttpContext.RequestAborted);
    }
}