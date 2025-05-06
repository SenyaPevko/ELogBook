using System.Net;
using Domain.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.CreationArgs;
using Infrastructure.WriteContext;

namespace Infrastructure.Commands.Base;

public abstract class CreateCommandBase<TDto, TEntity, TCreationArgs, TInvalidReason>(
    IRepository<TEntity, TInvalidReason> repository)
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
        // todo: валидация прав ? - access checker, и валидация прав строительного объекта в validator в рамках репы
        if (args.Id == Guid.Empty)
            return new CreateErrorInfo<TInvalidReason>(
                $"{typeof(TEntity).Name} creation error",
                $"Invalid id {args.Id}",
                HttpStatusCode.BadRequest);

        var existingEntity = await repository.GetByIdAsync(args.Id, cancellationToken);

        if (existingEntity is not null)
            return await MapToDtoAsync(existingEntity);

        var entity = await MapToEntityAsync(args);

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