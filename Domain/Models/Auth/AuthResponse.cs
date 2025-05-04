using Domain.Dtos;

namespace Domain.Models.Auth;

public class AuthResponse : EntityDto
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTime TokenExpiry { get; set; }
}