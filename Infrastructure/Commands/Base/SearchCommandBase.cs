using Domain.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;

namespace Infrastructure.Commands.Base;

public abstract class SearchCommandBase<TDto, TEntity>(
    IRepository<TEntity> repository)
    : CommandBase<TDto, TEntity>, ISearchCommand<TDto>
    where TDto : EntityDto
    where TEntity : EntityInfo, new()
{
    public async Task<ActionResult<List<TDto>, ErrorInfo>> ExecuteAsync(SearchRequest searchRequest,
        CancellationToken cancellationToken)
    {
        // todo: валидация прав
        
        // todo: перед поиском нужно валидировать запрос поиска, это логика стореджа
        var entities = await repository.SearchAsync(searchRequest, cancellationToken);
        var dtos = new List<TDto>(entities.Count);
        
        foreach (var entity in entities)
            dtos.Add(await MapToDtoAsync(entity));
        
        return dtos;
    }
}