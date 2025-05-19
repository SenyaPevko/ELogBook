using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.Users;

public class UserSearchRequest : SearchRequestBase
{
    public List<string>? Emails { get; set; }
    public string? Email { get; set; }
    public string? RefreshToken { get; set; }
}