using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.Users;

public class UserUpdateArgs : EntityUpdateArgs
{
    /// <summary>
    ///     Имя
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Фамилия
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    ///     Отчество
    /// </summary>
    public string? Patronymic { get; set; }
    
    /// <summary>
    ///     Номер телефона
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    ///     Id организации - проставляет админ при первой выдаче прав
    /// </summary>
    public Guid? OrganizationId { get; set; }
}