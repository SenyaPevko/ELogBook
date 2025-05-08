using Domain.Dtos;
using Domain.Entities.Users;
using Domain.Repository;
using Domain.RequestArgs.Users;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Users;

// todo: обновлять пользователя должен либо пользователь либо админ
public class UpdateUserCommand(
    IRepository<User, InvalidUserReason> repository)
    : UpdateCommandBase<UserDto, User,
        UserUpdateArgs, InvalidUserReason>(repository)
{
    protected override async Task<UserDto> MapToDtoAsync(User entity)
    {
        return await entity.ToDto();
    }
    
    protected override Task ApplyUpdatesAsync(User entity, UserUpdateArgs args)
    {
        if (args.Name is not null) entity.Name = args.Name;
        if (args.Surname is not null) entity.Surname = args.Surname;
        if (args.Patronymic is not null) entity.Patronymic = args.Patronymic;
        // todo: тут нужен afterWrite чтобы после обновления добавить пользователя в организацию,
        // а если организация сменилась, то из старой нужно удалить
        if (args.OrganizationId is not null) entity.OrganizationId = args.OrganizationId;

        return Task.CompletedTask;
    }
}