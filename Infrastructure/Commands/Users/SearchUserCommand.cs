using Domain.AccessChecker;
using Domain.Dtos;
using Domain.Entities.Users;
using Domain.Repository;
using Domain.RequestArgs.Users;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Users;

public class SearchUserCommand(
    IRepository<User, InvalidUserReason, UserSearchRequest> repository,
    IAccessChecker<User> accessChecker)
    : SearchCommandBase<UserDto, User, InvalidUserReason, UserSearchRequest>(repository, accessChecker)
{
    protected override async Task<UserDto> MapToDtoAsync(User entity)
    {
        return await entity.ToDto();
    }
}