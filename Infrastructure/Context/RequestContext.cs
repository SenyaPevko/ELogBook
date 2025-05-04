using Domain.Models.Auth;

namespace Infrastructure.Context;

public class RequestContext : IRequestContext
{
    public required AuthInfo Auth { get; set; }
    public DateTimeOffset RequestTime { get; set; }
}