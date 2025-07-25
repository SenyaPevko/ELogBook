using Domain.Dtos;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.RequestArgs.Base;

namespace Domain.Commands;

public interface ICreateCommand<TDto, in TCreationArgs, TInvalidReason>
    where TDto : EntityDto
    where TInvalidReason : Enum
    where TCreationArgs : IEntityCreationArgs
{
    public Task<ActionResult<TDto, CreateErrorInfo<TInvalidReason>>> ExecuteAsync(TCreationArgs args,
        CancellationToken cancellationToken);
}