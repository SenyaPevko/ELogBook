using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Dbo;

/// <summary>
/// Строительный объект
/// </summary>
[Table("constructionSites")]
public class ConstructionSiteDbo : EntityDbo
{
    /// <summary>
    /// Название объекта
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Название объекта
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// Название объекта
    /// </summary>
    public required string Address { get; set; }
    
    /// <summary>
    /// Изображение
    /// </summary>
    public required Uri Image { get; set; }
    
    /// <summary>
    /// Идентификатор листа регистрации
    /// </summary>
    public required Guid RegistrationSheetId { get; set; }
    
    /// <summary>
    /// Идентификатор учетного листа
    /// </summary>
    public required Guid RecordSheetId { get; set; }
    
    /// <summary>
    /// Приказы
    /// </summary>
    public List<Uri> Orders { get; set; } = [];
    
    /// <summary>
    /// Пользователи и их роли в этом проекте
    /// </summary>
    public List<Guid> ObjectUserRoleIds { get; set; } = []; 
}