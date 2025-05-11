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

namespace Infrastructure.Commands.Base;

public abstract class CreateCommandBase<TDto, TEntity, TCreationArgs, TInvalidReason>(
    IRepository<TEntity, TInvalidReason> repository,
    IAccessChecker<TEntity> accessChecker)
    : CommandBase<TDto, TEntity>, ICreateCommand<TDto, TCreationArgs, TInvalidReason>
    where TDto : EntityDto
    where TInvalidReason : Enum
    where TEntity : EntityInfo, new()
    where TCreationArgs : EntityCreationArgs
{
    public virtual async Task<ActionResult<TDto, CreateErrorInfo<TInvalidReason>>> ExecuteAsync(
        TCreationArgs args,
        CancellationToken cancellationToken)
    {
        var canCreate = await accessChecker.CanCreate();
        if (canCreate is false)
            return ErrorInfoExtensions.CreateAccessForbidden<TEntity, TInvalidReason>();
        
        if (args.Id == Guid.Empty)
            return new CreateErrorInfo<TInvalidReason>(
                $"{typeof(TEntity).Name} creation error",
                $"Invalid id {args.Id}",
                HttpStatusCode.BadRequest);

        var existingEntity = await repository.GetByIdAsync(args.Id, cancellationToken);

        if (existingEntity is not null)
            return await MapToDtoAsync(existingEntity);

        var entity = await MapToEntityAsync(args);
        if (canCreate is not true && !await accessChecker.CanCreate(entity))
            return ErrorInfoExtensions.CreateAccessForbidden<TEntity, TInvalidReason>();
        
        var writeContext = new WriteContext<TInvalidReason>();
        await repository.AddAsync(entity, writeContext, cancellationToken);

        if (!writeContext.IsSuccess)
            return new CreateErrorInfo<TInvalidReason>($"{typeof(TEntity).Name} creation error", "Invalid request data",
                HttpStatusCode.Conflict)
            {
                Errors = writeContext.Errors
            };

        return await MapToDtoAsync(entity);
    }

    protected abstract Task<TEntity> MapToEntityAsync(TCreationArgs args);
}