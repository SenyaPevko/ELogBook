using Domain.Entities.Users;

namespace Infrastructure.Storage;

public static class Helper
{
    public static string GetSignature(this User user)
    {
        return $"{user.Surname} {user.Name[0]}.{user.Patronymic[0]}.";
    }
}