namespace Domain.RequestArgs.Auth;

public class RevokeTokenRequest
{
    public required string RefreshToken { get; set; }
}