using Domain.Dtos;
using Domain.Entities.Users;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Users;

public class SearchUserCommand(IRepository<User, InvalidUserReason, UserSearchRequest> repository) 
    : SearchCommandBase<UserDto, User, InvalidUserReason, UserSearchRequest>(repository)
{
    protected override async Task<UserDto> MapToDtoAsync(User entity)
    {
        return await entity.ToDto();
    }
}