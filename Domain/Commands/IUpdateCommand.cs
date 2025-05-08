using Domain.Dtos;
using Domain.Entities;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.Base;

namespace Domain.Commands;

public interface IUpdateCommand<TDto, in TUpdateArgs, TInvalidReason>
    where TDto : EntityDto
    where TInvalidReason : Enum
    where TUpdateArgs : IEntityUpdateArgs
{
    public Task<ActionResult<TDto, UpdateErrorInfo<TInvalidReason>>> ExecuteAsync(Guid id, TUpdateArgs updateArgs,
        CancellationToken cancellationToken);
}