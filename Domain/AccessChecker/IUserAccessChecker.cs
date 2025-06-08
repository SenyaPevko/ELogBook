using Domain.Entities.Users;
using Domain.RequestArgs.Users;

namespace Domain.AccessChecker;

public interface IUserAccessChecker : IAccessChecker<User, UserUpdateArgs>
{
    bool CanUpdateOrganization();
}