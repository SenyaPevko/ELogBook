using System.Net;
using Domain.AccessChecker;
using Domain.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.Base;
using Infrastructure.Helpers;
using Infrastructure.WriteContext;
using Utils;

namespace Infrastructure.Commands.Base;

public abstract class UpdateCommandBase<TDto, TEntity, TUpdateArgs, TInvalidReason>(
    IRepository<TEntity, TInvalidReason> repository,
    IAccessChecker<TEntity, TUpdateArgs> accessChecker)
    : CommandBase<TDto, TEntity>, IUpdateCommand<TDto, TUpdateArgs, TInvalidReason>
    where TDto : EntityDto
    where TInvalidReason : Enum
    where TEntity : EntityInfo, new()
    where TUpdateArgs : IEntityUpdateArgs
{
    public virtual async Task<ActionResult<TDto, UpdateErrorInfo<TInvalidReason>>> ExecuteAsync(
        TUpdateArgs updateArgs,
        CancellationToken cancellationToken)
    {
        var canUpdate = await accessChecker.CanUpdate();
        if (canUpdate is false)
            return ErrorInfoExtensions.UpdateAccessForbidden<TEntity, TInvalidReason>(updateArgs.Id);

        var existingEntity = await repository.GetByIdAsync(updateArgs.Id, cancellationToken);
        if (existingEntity is null)
            return new UpdateErrorInfo<TInvalidReason>(
                $"Could not update {typeof(TEntity).Name}",
                $"Could not find {typeof(TEntity).Name} with id {updateArgs.Id}",
                HttpStatusCode.NotFound);

        if (canUpdate is not true && !await accessChecker.CanUpdate(existingEntity))
            return ErrorInfoExtensions.UpdateAccessForbidden<TEntity, TInvalidReason>(updateArgs.Id);

        var newEntity = existingEntity.Copy();
        await ApplyUpdatesAsync(newEntity, updateArgs);

        if (canUpdate is not true && !await accessChecker.CanUpdate(updateArgs, existingEntity, newEntity))
            return ErrorInfoExtensions.UpdateAccessForbidden<TEntity, TInvalidReason>(updateArgs.Id);

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