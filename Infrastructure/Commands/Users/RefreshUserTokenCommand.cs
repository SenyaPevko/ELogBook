using System.Net;
using System.Security.Claims;
using Domain.Auth;
using Domain.Entities.Users;
using Domain.Models.Auth;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.Repository;
using Domain.RequestArgs.Auth;
using Infrastructure.WriteContext;

namespace Infrastructure.Commands.Users;

public class RefreshUserTokenCommand(
    IRepository<User, InvalidUserReason> repository,
    IAuthService authService)
{
    public async Task<ActionResult<AuthResponse, UpdateErrorInfo<InvalidUserReason>>> ExecuteAsync(
        RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var principal = authService.ValidateToken(request.Token, false);
        if (principal == null)
            return new UpdateErrorInfo<InvalidUserReason>("Could not refresh token", "Token is invalid",
                HttpStatusCode.Conflict);

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return new UpdateErrorInfo<InvalidUserReason>("Could not refresh token", "Token claims are invalid",
                HttpStatusCode.Conflict);

        var user = await repository.GetByIdAsync(Guid.Parse(userId), cancellationToken);
        if (user == null || user.RefreshToken != request.RefreshToken ||
            user.RefreshTokenExpiry <= DateTimeOffset.UtcNow)
            return new UpdateErrorInfo<InvalidUserReason>("Could not refresh token", "Refresh token is invalid",
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