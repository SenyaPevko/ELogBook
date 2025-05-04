using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Auth;

public class PasswordHasher : IPasswordHasher<User>
{
    private readonly PasswordHasher<User> _hasher = new();

    public string HashPassword(User user, string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
        return _hasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
    }
}