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
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Описнаие объекта
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    ///     Адрес объекта
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    ///     Изображение
    /// </summary>
    public string Image { get; set; } = null!;

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
}