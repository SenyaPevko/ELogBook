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
    [AllowAnonymous]
    public async Task<ActionResult<GlobalPermission>> GetUserGlobalPermissionsAsync([FromServices] IPermissionService service)
    {
        return await service.GetUserGlobalPermissionsAsync(HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Разрешения в рамках строительного объекта
    /// </summary>
    /// <param name="service"></param>
    /// <param name="constructionSiteId"></param>
    /// <param name="recordSheetItemId"></param>
    /// <param name="registrationSheetItemId"></param>
    /// <returns></returns>
    [HttpGet("constructionSite/{constructionSiteId:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<ConstructionSitePermission>> GetUserConstructionSitePermissionsAsync(
        [FromServices] IPermissionService service,
        [FromRoute] Guid? constructionSiteId,
        [FromQuery] Guid? recordSheetItemId,
        [FromQuery] Guid? registrationSheetItemId)
    {
        return await service.GetUserConstructionSitePermissionsAsync(
            constructionSiteId,
            recordSheetItemId,
            registrationSheetItemId,
            HttpContext.RequestAborted);
    }
}