using Domain.Entities.Roles;

namespace Domain.Dtos;

public class UserDto : EntityDto
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
    ///     Номер телефона
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    ///     Название организации
    /// </summary>
    public string? OrganizationName { get; set; } = null!;

    /// <summary>
    ///     Id организации - проставляет админ при первой выдаче прав
    /// </summary>
    public Guid? OrganizationId { get; set; }

    /// <summary>
    ///     Роль в рамках сайта
    /// </summary>
    public UserRole UserRole { get; set; }
}