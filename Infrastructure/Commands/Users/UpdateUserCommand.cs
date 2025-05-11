using Domain.AccessChecker;
using Domain.Dtos;
using Domain.Entities.Users;
using Domain.Repository;
using Domain.RequestArgs.Users;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Users;

// todo: обновлять пользователя должен либо пользователь либо админ
public class UpdateUserCommand(
    IRepository<User, InvalidUserReason> repository,
    IAccessChecker<User, UserUpdateArgs> accessChecker)
    : UpdateCommandBase<UserDto, User,
        UserUpdateArgs, InvalidUserReason>(repository, accessChecker)
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
        if (args.OrganizationId is not null) entity.OrganizationId = args.OrganizationId;

        return Task.CompletedTask;
    }
}