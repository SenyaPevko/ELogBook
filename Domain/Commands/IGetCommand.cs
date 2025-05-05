using Domain.Dtos;
using Domain.Entities;
using Domain.Models.ErrorInfo;

namespace Domain.Commands;

public interface IGetCommand<TDto>
    where TDto : EntityDto
{
    public Task<ActionResult<TDto, ErrorInfo>> ExecuteAsync(Guid id, CancellationToken cancellationToken);
}