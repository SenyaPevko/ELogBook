using System.Net;
using Domain.AccessChecker;
using Domain.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Infrastructure.Helpers;

namespace Infrastructure.Commands.Base;

public abstract class GetCommandBase<TDto, TEntity>(
    IRepository<TEntity> repository,
    IAccessChecker<TEntity> accessChecker)
    : CommandBase<TDto, TEntity>, IGetCommand<TDto>
    where TDto : EntityDto
    where TEntity : EntityInfo, new()
{
    public async Task<ActionResult<TDto, ErrorInfo>> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var canRead = await accessChecker.CanRead();
        if (canRead is false)
            return ErrorInfoExtensions.ReadAccessForbidden<TEntity>();
        
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return new ErrorInfo(
                $"Could not get {typeof(TEntity).Name}",
                $"{typeof(TEntity).Name} with id {id.ToString()} not found)",
                HttpStatusCode.NotFound);
        
        if (canRead is not true && !await accessChecker.CanRead(entity))
            return ErrorInfoExtensions.ReadAccessForbidden<TEntity>();

        return await MapToDtoAsync(entity);
    }
}