using System.ComponentModel.DataAnnotations.Schema;
using Api.Models.Models.Roles;

namespace Domain.Dbo;

[Table("users")]
public class UserDbo : EntityDbo
{
    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public required string Surname { get; set; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    public required string Patronymic { get; set; }
    
    /// <summary>
    /// Почтовый адрес
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Название организации
    /// </summary>
    public required string OrganizationName { get; set; }
    
    /// <summary>
    /// Id организации - проставляет админ при первой выдаче прав
    /// </summary>
    public Guid? OrganizationId { get; set; } 
    
    /// <summary>
    /// Роль в рамках сайта
    /// </summary>
    public required UserRole UserRole { get; set; } 
}