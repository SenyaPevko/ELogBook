using Domain.Entities.Base;

namespace Domain.Entities.ConstructionSite;

public class ConstructionSite : EntityInfo
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
    public RegistrationSheet.RegistrationSheet RegistrationSheet { get; set; } = null!;

    /// <summary>
    ///     Идентификатор учетного листа
    /// </summary>
    public RecordSheet.RecordSheet RecordSheet { get; set; } = null!;

    /// <summary>
    ///     Приказы
    /// </summary>
    public List<Uri> Orders { get; set; } = [];

    /// <summary>
    ///     Пользователи и их роли в этом проекте
    /// </summary>
    public List<ConstructionSiteUserRole> ConstructionSiteUserRoleIds { get; set; } = [];
}