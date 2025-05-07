using Domain.Dtos;
using Domain.Entities.Users;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Users;

public class SearchUserCommand(IRepository<User> repository) : SearchCommandBase<UserDto, User>(repository)
{
    protected override async Task<UserDto> MapToDtoAsync(User entity)
    {
        return await entity.ToDto();
    }
}