using Domain.Commands;
using Domain.Dtos;
using Domain.Dtos.ConstructionSite;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Roles;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.RequestArgs.ConstructionSites;
using ELogBook.Controllers.Base;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ConstructionSite = Domain.Entities.ConstructionSite.ConstructionSite;

namespace ELogBook.Controllers;

public class ConstructionSitesController(IRequestContext context)
    : CreatableEntityControllerBase<ConstructionSiteDto, ConstructionSite, ConstructionSiteCreationArgs,
        ConstructionSiteUpdateArgs,
        InvalidConstructionSiteReason, ConstructionSiteSearchRequest>
{
    /// <summary>
    ///     Получить доступные объекты
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("accessible")]
    public async Task<ActionResult<List<ConstructionSiteDto>, ErrorInfo>> Search(
        [FromServices] ISearchCommand<ConstructionSiteDto, ConstructionSiteSearchRequest> command)
    {
        var searchRequest = new ConstructionSiteSearchRequest { UserRoleUserId = context.Auth.UserId };
        if (context.Auth.Role is UserRole.Admin)
        {
            searchRequest.UserRoleUserId = null;
            searchRequest.GetAll = true;
        }


        return await command.ExecuteAsync(searchRequest, HttpContext.RequestAborted);
    }
}