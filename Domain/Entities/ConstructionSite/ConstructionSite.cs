using Domain.Entities.Base;
using Domain.Entities.WorkIssues;

namespace Domain.Entities.ConstructionSite;

public class ConstructionSite : EntityInfo
{
    /// <summary>
    ///     Название объекта
    /// </summary>
    public string ShortName { get; set; } = null!;

    /// <summary>
    ///     Описание объекта
    /// </summary>
    public string FullName { get; set; } = null!;

    /// <summary>
    ///     Адрес объекта
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    ///     Лист регистрации
    /// </summary>
    public RegistrationSheet.RegistrationSheet RegistrationSheet { get; set; } = null!;

    /// <summary>
    ///     Учетный лист
    /// </summary>
    public RecordSheet.RecordSheet RecordSheet { get; set; } = null!;

    /// <summary>
    ///     Рабочие вопросы
    /// </summary>
    public WorkIssue WorkIssue { get; set; } = null!;

    /// <summary>
    ///     Приказы
    /// </summary>
    public List<Order> Orders { get; set; } = [];

    /// <summary>
    ///     Пользователи и их роли в этом проекте
    /// </summary>
    public List<ConstructionSiteUserRole> ConstructionSiteUserRoles { get; set; } = [];
    
    /// <summary>
    ///     Генеральный подрядчик - Id организации
    /// </summary>
    public Guid OrganizationId { get; set; }
    
    /// <summary>
    ///     Исполнители отдельных видов работ (субподрядчики) - наименование работ и название организации
    /// </summary>
    public Guid SubOrganizationId { get; set; }
}