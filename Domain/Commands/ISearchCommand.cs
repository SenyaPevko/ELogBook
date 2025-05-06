using Domain.Dtos;
using Domain.Entities;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.SearchRequest;

namespace Domain.Commands;

public interface ISearchCommand<TDto>
    where TDto : EntityDto
{
    public Task<ActionResult<List<TDto?>, ErrorInfo>> ExecuteAsync(SearchRequest searchRequest,
        CancellationToken cancellationToken);
}