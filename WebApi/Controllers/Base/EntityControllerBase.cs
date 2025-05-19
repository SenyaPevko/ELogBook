using Domain.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.RequestArgs.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELogBook.Controllers.Base;

// todo: нехватает delete метода, проверить, потому что он вроде может быть нужен
[ApiController]
[Route("api/" + "[controller]")]
[Authorize]
public abstract class EntityControllerBase<TDto, TEntity, TUpdateArgs, TInvalidReason, TSearchRequest> : ControllerBase
    where TDto : EntityDto
    where TInvalidReason : Enum
    where TEntity : EntityInfo
    where TUpdateArgs : IEntityUpdateArgs
    where TSearchRequest : SearchRequestBase
{
    /// <summary>
    ///     Получить по id
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TDto, ErrorInfo>> Get([FromServices] IGetCommand<TDto> command, [FromRoute] Guid id)
    {
        return await command.ExecuteAsync(id, HttpContext.RequestAborted);
    }

    /// <summary>
    ///     Поиск
    /// </summary>
    /// <param name="command"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<TDto>, ErrorInfo>> Search(
        [FromServices] ISearchCommand<TDto, TSearchRequest> command,
        [FromQuery] TSearchRequest request)
    {
        return await command.ExecuteAsync(request, HttpContext.RequestAborted);
    }

    /// <summary>
    ///     Обновить
    /// </summary>
    /// <param name="command"></param>
    /// <param name="request"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<TDto, UpdateErrorInfo<TInvalidReason>>> Update(
        [FromServices] IUpdateCommand<TDto, TUpdateArgs, TInvalidReason> command,
        [FromBody] TUpdateArgs request,
        [FromRoute] Guid id)
    {
        request.Id = id;

        return await command.ExecuteAsync(request, HttpContext.RequestAborted);
    }
}