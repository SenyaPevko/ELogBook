using System.Security.Claims;
using Domain.Entities.Roles;
using Domain.Entities.Users;

namespace Domain.Auth;

public interface IJwtService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token, bool validateLifeTime);
    string? GetUserIdFromToken(string token);
    string? GetEmailFromToken(string token);
    UserRole? GetUserRoleFromToken(string token);
    int GetTokenExpiryMinutes();
}