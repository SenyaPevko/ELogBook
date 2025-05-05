using System.Security.Claims;
using Domain.Auth;
using Domain.Entities.Users;
using Domain.Models.Auth;

namespace Infrastructure.Auth;

public class AuthService(IJwtService jwtService) : IAuthService
{
    public AuthResponse GenerateTokens(User user)
    {
        var token = jwtService.GenerateToken(user);
        var refreshToken = jwtService.GenerateRefreshToken();

        return new AuthResponse
        {
            Token = token,
            RefreshToken = refreshToken,
            TokenExpiry = DateTime.UtcNow.AddMinutes(jwtService.GetTokenExpiryMinutes()),
            Id = user.Id
        };
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        return jwtService.ValidateToken(token);
    }
}