using Domain.Dtos;
using Domain.Entities.Users;
using Domain.RequestArgs.UpdateArgs;
using ELogBook.Controllers.Base;

namespace ELogBook.Controllers;

public class UsersController : EntityControllerBase<UserDto, User, UserUpdateArgs, InvalidUserReason>
{
}