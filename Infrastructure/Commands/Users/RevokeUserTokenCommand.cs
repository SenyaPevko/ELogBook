using System.Net;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.Repository;
using Domain.RequestArgs.Auth;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.WriteContext;

namespace Infrastructure.Commands.Users;

public class RevokeUserTokenCommand(IRepository<User, InvalidUserReason, UserSearchRequest> repository)
{
    public async Task<ActionResult<bool, UpdateErrorInfo<InvalidUserReason>>> ExecuteAsync(
        RevokeTokenRequest request,
        CancellationToken cancellationToken)
    {
        var user = (await repository.SearchAsync(new UserSearchRequest { RefreshToken = request.RefreshToken },
            cancellationToken)).FirstOrDefault();
        if (user is null)
            return new UpdateErrorInfo<InvalidUserReason>("Could not revoke token",
                "Could not find user with refresh token", HttpStatusCode.Conflict);

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        var writeContext = new WriteContext<InvalidUserReason>();
        await repository.UpdateAsync(user, writeContext, cancellationToken);

        if (!writeContext.IsSuccess)
            return new UpdateErrorInfo<InvalidUserReason>("Could not login user", "Failed trying to update user",
                HttpStatusCode.Conflict)
            {
                Errors = writeContext.Errors
            };

        return true;
    }
}