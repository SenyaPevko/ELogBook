using System.Net;
using Domain.Auth;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Models.Auth;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.Auth;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Helpers.SearchRequestHelper;
using Infrastructure.WriteContext;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Commands.Users;

public class LoginUserCommand(
    IRepository<User, InvalidUserReason> repository,
    IPasswordHasher<User> passwordHasher,
    IAuthService authService)
{
    public async Task<ActionResult<AuthResponse, UpdateErrorInfo<InvalidUserReason>>> ExecuteAsync(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var user = (await repository.SearchAsync(
            new SearchRequest().WhereEquals<User, string>(u => u.Email, request.Email)
                .SinglePage(),
            cancellationToken)).FirstOrDefault();
        if (user is null)
            return new UpdateErrorInfo<InvalidUserReason>(
                "Could not login user",
                $"Could not find user with email {request.Email}",
                HttpStatusCode.NotFound);

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result != PasswordVerificationResult.Success)
            return new UpdateErrorInfo<InvalidUserReason>(
                "Could not login user",
                $"Password {request.Password} is invalid",
                HttpStatusCode.Conflict);

        var tokens = authService.GenerateTokens(user);
        user.RefreshToken = tokens.RefreshToken;
        user.RefreshTokenExpiry = DateTimeOffset.UtcNow.AddDays(7);

        var writeContext = new WriteContext<InvalidUserReason>();
        await repository.UpdateAsync(user, writeContext, CancellationToken.None);

        if (!writeContext.IsSuccess)
            return new UpdateErrorInfo<InvalidUserReason>("Could not login user", "Failed trying to update user",
                HttpStatusCode.Conflict)
            {
                Errors = writeContext.Errors
            };

        return tokens;
    }
}