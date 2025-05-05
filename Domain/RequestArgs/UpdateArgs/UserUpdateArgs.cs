namespace Domain.RequestArgs.UpdateArgs;

public class UserUpdateArgs : IEntityUpdateArgs
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
    ///     Id организации - проставляет админ при первой выдаче прав
    /// </summary>
    // todo: при обновлении этого поля должны быть права админа
    public Guid? OrganizationId { get; set; }
}