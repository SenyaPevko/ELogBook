using Domain.Entities.Base;
using Domain.Entities.Users;

namespace Domain.Entities;

public class Organization : EntityInfo
{
    /// <summary>
    ///     Название организации
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Id пользователей организации
    /// </summary>
    public List<User> Users { get; set; } = [];
}