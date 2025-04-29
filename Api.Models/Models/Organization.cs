namespace Api.Models.Models;

public class Organization : EntityInfo
{
    /// <summary>
    /// Название организации
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Id пользователей организации
    /// </summary>
    public List<User> Users { get; set; } = [];
}