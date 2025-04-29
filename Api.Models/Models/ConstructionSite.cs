namespace Api.Models.Models;

public class ConstructionSite : EntityInfo
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
    public required RegistrationSheet.RegistrationSheet RegistrationSheet { get; set; }
    
    /// <summary>
    /// Идентификатор учетного листа
    /// </summary>
    public required RecordSheet.RecordSheet RecordSheetId { get; set; }
    
    /// <summary>
    /// Приказы
    /// </summary>
    public List<Uri> Orders { get; set; } = [];
    
    /// <summary>
    /// Пользователи и их роли в этом проекте
    /// </summary>
    public List<ObjectUserRole> ObjectUserRoleIds { get; set; } = []; 
}