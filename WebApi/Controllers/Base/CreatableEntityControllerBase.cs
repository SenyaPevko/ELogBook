using Domain.Commands;
using Domain.Dtos;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.RequestArgs.Base;
using Microsoft.AspNetCore.Mvc;

namespace ELogBook.Controllers.Base;

public abstract class CreatableEntityControllerBase<TDto, TEntity, TCreationArgs, TUpdateArgs, TInvalidReason,
    TSearchRequest>
    : EntityControllerBase<TDto, TEntity, TUpdateArgs, TInvalidReason, TSearchRequest>
    where TDto : EntityDto
    where TInvalidReason : Enum
    where TEntity : EntityInfo
    where TCreationArgs : EntityCreationArgs
    where TUpdateArgs : IEntityUpdateArgs
    where TSearchRequest : SearchRequestBase
{
    /// <summary>
    ///     Создать
    /// </summary>
    /// <param name="command"></param>
    /// <param name="args"></param>
    [HttpPost]
    public async Task<ActionResult<TDto, CreateErrorInfo<TInvalidReason>>> Create(
        [FromServices] ICreateCommand<TDto, TCreationArgs, TInvalidReason> command,
        [FromBody] TCreationArgs args)
    {
        var id = Guid.NewGuid();
        args.Id = id;

        return await command.ExecuteAsync(args, HttpContext.RequestAborted);
    }
}