using Domain.Entities.Roles;

namespace Domain.Models.Auth;

public class AuthInfo
{
    public Guid? UserId { get; set; }
    public string? Email { get; set; }
    public UserRole? Role { get; set; }
}