using Domain.Commands;
using Domain.Dtos;
using Domain.Entities.Organization;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.RequestArgs.Organizations;
using ELogBook.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELogBook.Controllers;

public class OrganizationsController
    : CreatableEntityControllerBase<OrganizationDto, Organization, OrganizationCreationArgs, OrganizationUpdateArgs,
        InvalidOrganizationReason, OrganizationSearchRequest>
{
    /// <summary>
    ///     Поиск
    /// </summary>
    /// <param name="command"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public override async Task<ActionResult<List<OrganizationDto>, ErrorInfo>> Search(
        [FromServices] ISearchCommand<OrganizationDto, OrganizationSearchRequest> command,
        [FromQuery] OrganizationSearchRequest request)
    {
        return await command.ExecuteAsync(request, HttpContext.RequestAborted);
    }
}