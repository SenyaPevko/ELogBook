using Domain.Dtos;
using Domain.Entities;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.SearchRequest;

namespace Domain.Commands;

public interface ISearchCommand<TDto, in TSearchRequest>
    where TDto : EntityDto
    where TSearchRequest : SearchRequestBase
{
    public Task<ActionResult<List<TDto>, ErrorInfo>> ExecuteAsync(TSearchRequest searchRequest,
        CancellationToken cancellationToken);
}