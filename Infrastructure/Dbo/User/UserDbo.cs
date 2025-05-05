using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Roles;

namespace Infrastructure.Dbo.User;

[Table("users")]
public class UserDbo : EntityDbo
{
    /// <summary>
    ///     Имя
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Фамилия
    /// </summary>
    public string Surname { get; set; } = null!;

    /// <summary>
    ///     Отчество
    /// </summary>
    public string Patronymic { get; set; } = null!;

    /// <summary>
    ///     Почтовый адрес
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    ///     Название организации
    /// </summary>
    public string OrganizationName { get; set; } = null!;

    /// <summary>
    ///     Id организации - проставляет админ при первой выдаче прав
    /// </summary>
    public Guid? OrganizationId { get; set; }

    /// <summary>
    ///     Роль в рамках сайта
    /// </summary>
    public UserRole UserRole { get; set; }

    public string PasswordHash { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpiry { get; set; }
}