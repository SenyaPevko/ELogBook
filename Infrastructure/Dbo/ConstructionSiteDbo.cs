using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Dbo;

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
    ///     Название объекта
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    ///     Название объекта
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    ///     Изображение
    /// </summary>
    public Uri Image { get; set; } = null!;

    /// <summary>
    ///     Идентификатор листа регистрации
    /// </summary>
    public Guid RegistrationSheetId { get; set; }

    /// <summary>
    ///     Идентификатор учетного листа
    /// </summary>
    public Guid RecordSheetId { get; set; }

    /// <summary>
    ///     Приказы
    /// </summary>
    public List<Uri> Orders { get; set; } = [];

    /// <summary>
    ///     Пользователи и их роли в этом проекте
    /// </summary>
    public List<Guid> ConstructionSiteUserRoleIds { get; set; } = [];
}