using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Auth;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth;

public class JwtService(IOptions<JwtSettings> settings) : IJwtService
{
    private readonly JwtSettings _settings = settings.Value;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.UserRole.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _settings.Issuer,
            _settings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
            signingCredentials: creds);

        return _tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        try
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _settings.Issuer,
                ValidateAudience = true,
                ValidAudience = _settings.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = key,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };

            return _tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch
        {
            return null;
        }
    }

    public int GetTokenExpiryMinutes()
    {
        return _settings.ExpiryMinutes;
    }

    public string? GetUserIdFromToken(string token)
    {
        var principal = ValidateToken(token);

        return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public string? GetEmailFromToken(string token)
    {
        var principal = ValidateToken(token);

        return principal?.FindFirst(ClaimTypes.Email)?.Value;
    }

    public UserRole? GetUserRoleFromToken(string token)
    {
        var principal = ValidateToken(token);
        if (Enum.TryParse<UserRole>(principal?.FindFirst(ClaimTypes.Role)?.Value, out var role))
            return role;

        return null;
    }
}