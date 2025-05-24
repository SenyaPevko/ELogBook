using System.Security.Claims;
using Domain.Entities.Users;
using Domain.Models.Auth;

namespace Domain.Auth;

public interface IAuthService
{
    AuthResponse GenerateTokens(User user);

    ClaimsPrincipal? ValidateToken(string token, bool validateLifeTime = true);
}