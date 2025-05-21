using Core.Helpers;
using Domain.AccessChecker;
using Domain.Commands;
using Domain.Dtos;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.Repository;
using Domain.RequestArgs.Base;
using Infrastructure.Helpers;

namespace Infrastructure.Commands.Base;

public abstract class SearchCommandBase<TDto, TEntity, TInvalidReason, TSearchRequest>(
    IRepository<TEntity, TInvalidReason, TSearchRequest> repository,
    IAccessChecker<TEntity> accessChecker)
    : CommandBase<TDto, TEntity>, ISearchCommand<TDto, TSearchRequest>
    where TDto : EntityDto
    where TEntity : EntityInfo, new()
    where TSearchRequest : SearchRequestBase
    where TInvalidReason : Enum
{
    public async Task<ActionResult<List<TDto>, ErrorInfo>> ExecuteAsync(TSearchRequest searchRequest,
        CancellationToken cancellationToken)
    {
        var canRead = await accessChecker.CanRead();
        if (canRead is false)
            return ErrorInfoExtensions.ReadAccessForbidden<TEntity>();

        var entities = await repository.SearchAsync(searchRequest, cancellationToken);
        if (entities.Count > 0 && canRead is not true && !await accessChecker.CanRead(entities.First()))
            return ErrorInfoExtensions.ReadAccessForbidden<TEntity>();

        var dtos = await entities.SelectAsync(MapToDtoAsync);

        return dtos.ToList();
    }
}