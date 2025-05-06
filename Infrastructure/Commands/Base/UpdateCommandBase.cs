using System.Net;
using Domain.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.UpdateArgs;
using Infrastructure.WriteContext;
using Utils;

namespace Infrastructure.Commands.Base;

public abstract class UpdateCommandBase<TDto, TEntity, TUpdateArgs, TInvalidReason>(
    IRepository<TEntity, TInvalidReason> repository)
    : CommandBase<TDto, TEntity>, IUpdateCommand<TDto, TUpdateArgs, TInvalidReason>
    where TDto : EntityDto
    where TInvalidReason : Enum
    where TEntity : EntityInfo, new()
    where TUpdateArgs : IEntityUpdateArgs
{
    public virtual async Task<ActionResult<TDto, UpdateErrorInfo<TInvalidReason>>> ExecuteAsync(Guid id,
        TUpdateArgs updateArgs,
        CancellationToken cancellationToken)
    {
        // todo: валидация прав

        var existingEntity = await repository.GetByIdAsync(id, cancellationToken);
        if (existingEntity is null)
            return new UpdateErrorInfo<TInvalidReason>(
                $"Could not update {typeof(TEntity).Name}",
                $"Could not find {typeof(TEntity).Name} with id {id}",
                HttpStatusCode.NotFound);

        var newEntity = existingEntity.Copy();
        await ApplyUpdatesAsync(newEntity, updateArgs);

        // todo: валидация прав относительно изменений

        var writeContext = new WriteContext<TInvalidReason>();
        await repository.UpdateAsync(newEntity, writeContext, cancellationToken);

        if (!writeContext.IsSuccess)
            return new UpdateErrorInfo<TInvalidReason>($"Could not update {typeof(TEntity).Name}",
                "Invalid request data",
                HttpStatusCode.Conflict)
            {
                Errors = writeContext.Errors
            };

        return await MapToDtoAsync(newEntity);
    }

    protected abstract Task ApplyUpdatesAsync(TEntity entity, TUpdateArgs args);
}