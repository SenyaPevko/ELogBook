using Domain.Dtos.RecordSheet;
using Domain.Dtos.RegistrationSheet;
using Domain.Dtos.WorkIssue;
using Domain.Entities.ConstructionSite;

namespace Domain.Dtos.ConstructionSite;

public class ConstructionSiteDto : EntityDto
{
    /// <summary>
    ///     Краткое наименование объекта
    /// </summary>
    public required string ShortName { get; set; }

    /// <summary>
    ///     Наименование объекта в соответствии с документацией
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    ///     Название объекта
    /// </summary>
    public required string Address { get; set; }

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
    public List<OrderDto> Orders { get; set; } = [];

    /// <summary>
    ///     Пользователи и их роли в этом проекте
    /// </summary>
    public List<ConstructionSiteUserRole> ConstructionSiteUserRoles { get; set; } = [];
    
    /// <summary>
    ///     Генеральный подрядчик - организация
    /// </summary>
    public required OrganizationDto Organization { get; set; }
    
    /// <summary>
    ///     Исполнители отдельных видов работ (субподрядчики) - организация
    /// </summary>
    public required OrganizationDto SubOrganization { get; set; }
}