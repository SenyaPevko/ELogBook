using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.Auth;

public class RegisterRequest : EntityCreationArgs
{
    /// <summary>
    ///     Имя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Фамилия
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    ///     Отчество
    /// </summary>
    public required string Patronymic { get; set; }

    /// <summary>
    ///     Почтовый адрес
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    ///     Номер телефона
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    ///     Название организации
    /// </summary>
    public required string OrganizationName { get; set; }

    /// <summary>
    ///     Пароль
    /// </summary>
    public required string Password { get; set; }
}