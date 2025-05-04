namespace Domain.RequestArgs.Auth;

public class RefreshTokenRequest
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}