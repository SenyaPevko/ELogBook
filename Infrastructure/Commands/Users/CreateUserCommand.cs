using Domain.AccessChecker;
using Domain.Auth;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Models.Auth;
using Domain.Repository;
using Domain.RequestArgs.Auth;
using Infrastructure.Commands.Base;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Commands.Users;

public class CreateUserCommand(
    IRepository<User, InvalidUserReason> repository,
    IPasswordHasher<User> passwordHasher,
    IAuthService authService,
    IAccessChecker<User> accessChecker)
    : CreateCommandBase<AuthResponse, User, RegisterRequest, InvalidUserReason>(repository, accessChecker)
{
    protected override Task<User> MapToEntityAsync(RegisterRequest args)
    {
        var user = new User
        {
            Id = args.Id,
            Name = args.Name,
            Surname = args.Surname,
            Patronymic = args.Patronymic,
            Email = args.Email,
            OrganizationName = args.OrganizationName,
            UserRole = UserRole.Unknown
        };

        var tokens = authService.GenerateTokens(user);

        user.PasswordHash = passwordHasher.HashPassword(user, args.Password);
        user.Token = tokens.Token;
        user.TokenExpiry = tokens.TokenExpiry;
        user.RefreshToken = tokens.RefreshToken;
        user.RefreshTokenExpiry = DateTimeOffset.UtcNow.AddDays(7);

        return Task.FromResult(user);
    }

    protected override Task<AuthResponse> MapToDtoAsync(User entity)
    {
        return Task.FromResult(new AuthResponse
        {
            Id = entity.Id,
            Token = entity.Token!,
            RefreshToken = entity.RefreshToken!,
            TokenExpiry = entity.TokenExpiry!.Value
        });
    }
}