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
    public Task<ActionResult<List<TDto?>, ErrorInfo>> ExecuteAsync(SearchRequest searchRequest,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task<TDto> MapToDtoAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}