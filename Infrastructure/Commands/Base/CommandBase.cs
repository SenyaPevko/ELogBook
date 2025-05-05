using Domain.Dtos;
using Domain.Entities.Base;

namespace Infrastructure.Commands.Base;

public abstract class CommandBase<TDto, TEntity>
    where TDto : EntityDto
    where TEntity : EntityInfo, new()
{
    protected abstract Task<TDto> MapToDtoAsync(TEntity entity);
}