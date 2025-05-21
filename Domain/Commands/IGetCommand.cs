using Domain.Dtos;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;

namespace Domain.Commands;

public interface IGetCommand<TDto>
    where TDto : EntityDto
{
    public Task<ActionResult<TDto, ErrorInfo>> ExecuteAsync(Guid id, CancellationToken cancellationToken);
}