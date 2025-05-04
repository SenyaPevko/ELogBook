namespace Domain.Entities.Roles;

/// <summary>
///     Правовые роли пользователей в рамках объекта
/// </summary>
public enum ConstructionSiteUserRoleType
{
    /// <summary>
    ///     Администратор
    /// </summary>
    Admin = 1,

    /// <summary>
    ///     Заказчик
    /// </summary>
    Customer = 2,

    /// <summary>
    ///     Производитель работ
    /// </summary>
    Operator = 3,

    /// <summary>
    ///     Авторский надзор
    /// </summary>
    AuthorSupervision = 4,

    /// <summary>
    ///     Обозреватель
    /// </summary>
    Observer = 5
}