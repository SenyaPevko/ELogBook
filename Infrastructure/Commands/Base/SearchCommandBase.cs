using Core.Helpers;
using Domain.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;

namespace Infrastructure.Commands.Base;

public abstract class SearchCommandBase<TDto, TEntity, TInvalidReason, TSearchRequest>(
    IRepository<TEntity, TInvalidReason, TSearchRequest> repository)
    : CommandBase<TDto, TEntity>, ISearchCommand<TDto, TSearchRequest>
    where TDto : EntityDto
    where TEntity : EntityInfo, new()
    where TSearchRequest : SearchRequestBase
    where TInvalidReason : Enum
{
    public async Task<ActionResult<List<TDto>, ErrorInfo>> ExecuteAsync(TSearchRequest searchRequest,
        CancellationToken cancellationToken)
    {
        // todo: валидация прав
        
        var entities = await repository.SearchAsync(searchRequest, cancellationToken);
        var dtos = await entities.SelectAsync(MapToDtoAsync);
        
        return dtos.ToList();
    }
}