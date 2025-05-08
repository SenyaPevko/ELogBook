using Domain.Entities.Roles;

namespace Domain.Models.Auth;

public class AuthInfo
{
    public Guid UserId { get; init; }
    public UserRole Role { get; set; }
}