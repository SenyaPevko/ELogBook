using Domain.Dtos;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.RequestArgs.Base;

namespace Domain.Commands;

public interface IUpdateCommand<TDto, in TUpdateArgs, TInvalidReason>
    where TDto : EntityDto
    where TInvalidReason : Enum
    where TUpdateArgs : IEntityUpdateArgs
{
    public Task<ActionResult<TDto, UpdateErrorInfo<TInvalidReason>>> ExecuteAsync(TUpdateArgs updateArgs,
        CancellationToken cancellationToken);
}