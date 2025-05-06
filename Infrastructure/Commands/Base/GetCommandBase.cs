using System.Net;
using Domain.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Repository;

namespace Infrastructure.Commands.Base;

public abstract class GetCommandBase<TDto, TEntity>(
    IRepository<TEntity> repository)
    : CommandBase<TDto, TEntity>, IGetCommand<TDto>
    where TDto : EntityDto
    where TEntity : EntityInfo, new()
{
    public async Task<ActionResult<TDto, ErrorInfo>> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return new ErrorInfo(
                $"Could not get {typeof(TEntity).Name}",
                $"{typeof(TEntity).Name} with id {id.ToString()} not found)",
                HttpStatusCode.NotFound);

        return await MapToDtoAsync(entity);
    }
}