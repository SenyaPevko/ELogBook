using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.ConstructionSite;

namespace Infrastructure.Dbo.ConstructionSite;

/// <summary>
///     Строительный объект
/// </summary>
[Table("constructionSites")]
public class ConstructionSiteDbo : EntityDbo
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
    ///     Идентификатор листа регистрации
    /// </summary>
    public Guid RegistrationSheetId { get; set; }

    /// <summary>
    ///     Идентификатор учетного листа
    /// </summary>
    public Guid RecordSheetId { get; set; }

    /// <summary>
    ///     Идентификатор рабочих вопросов
    /// </summary>
    public Guid WorkIssueId { get; set; }

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