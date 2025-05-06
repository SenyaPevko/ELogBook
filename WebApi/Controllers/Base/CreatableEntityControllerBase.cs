using Domain.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.CreationArgs;
using Domain.RequestArgs.UpdateArgs;
using Microsoft.AspNetCore.Mvc;

namespace ELogBook.Controllers.Base;

public abstract class CreatableEntityControllerBase<TDto, TEntity, TCreationArgs, TUpdateArgs, TInvalidReason>
    : EntityControllerBase<TDto, TEntity, TUpdateArgs, TInvalidReason>
    where TDto : EntityDto
    where TInvalidReason : Enum
    where TEntity : EntityInfo
    where TCreationArgs : EntityCreationArgs
    where TUpdateArgs : IEntityUpdateArgs
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

        return await command.ExecuteAsync(id, args, HttpContext.RequestAborted);
    }
}