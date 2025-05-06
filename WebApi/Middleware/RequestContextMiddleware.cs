using System.Security.Claims;
using Domain.Entities.Roles;
using Domain.Models.Auth;
using Infrastructure.Context;

namespace ELogBook.Middleware;

public class RequestContextMiddleware(RequestDelegate next, ILogger<RequestContextMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext, AppDbContext dbContext)
    {
        var requestContext = new RequestContext
        {
            Auth = ExtractAuthInfo(httpContext.User),
            RequestTime = DateTimeOffset.UtcNow
        };

        RequestContextHolder.Current = requestContext;

        try
        {
            logger.LogInformation("Processing request {RequestId}", httpContext.TraceIdentifier);
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while processing request");
            throw;
        }
    }

    private static AuthInfo ExtractAuthInfo(ClaimsPrincipal user)
    {
        return new AuthInfo
        {
            UserId = Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
                ? userId
                : null,
            Role = Enum.TryParse<UserRole>(user.FindFirstValue(ClaimTypes.Role), out var role)
                ? role
                : UserRole.Unknown
        };
    }
}