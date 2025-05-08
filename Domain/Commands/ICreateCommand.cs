using Domain.Dtos;
using Domain.Entities;
using Domain.Models.ErrorInfo;
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