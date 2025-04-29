namespace Api.Models.Models.Roles;

/// <summary>
/// Правовые роли пользователей в рамках сайта
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Нет роли
    /// </summary>
    Unknown = 1, 
    
    /// <summary>
    /// Администратор
    /// </summary>
    Admin = 2,
}