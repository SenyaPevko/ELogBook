using Domain.Dtos.RecordSheet;
using Domain.Dtos.RegistrationSheet;
using Domain.Dtos.WorkIssue;
using Domain.Entities.ConstructionSite;

namespace Domain.Dtos;

public class ConstructionSiteDto : EntityDto
{
    /// <summary>
    ///     Название объекта
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Название объекта
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    ///     Название объекта
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    ///     Изображение
    /// </summary>
    public required Uri Image { get; set; }

    /// <summary>
    ///     Идентификатор листа регистрации
    /// </summary>
    public required RegistrationSheetDto RegistrationSheet { get; set; }

    /// <summary>
    ///     Идентификатор учетного листа
    /// </summary>
    public required RecordSheetDto RecordSheet { get; set; }
    
    /// <summary>
    ///     Рабочие вопросы
    /// </summary>
    public required WorkIssueDto WorkIssue { get; set; }

    /// <summary>
    ///     Приказы
    /// </summary>
    public List<Order> Orders { get; set; } = [];

    /// <summary>
    ///     Пользователи и их роли в этом проекте
    /// </summary>
    public List<ConstructionSiteUserRole> ConstructionSiteUserRoles { get; set; } = [];
}