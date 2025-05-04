using Domain.Models.Auth;

namespace Infrastructure.Context;

public interface IRequestContext
{
    public AuthInfo Auth { get; }
    public DateTimeOffset RequestTime { get; }
}