using Domain.Entities.Base;

namespace Domain.Entities.ConstructionSite;

public class ConstructionSite : EntityInfo
{
    /// <summary>
    ///     Название объекта
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Описание объекта
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    ///     Адрес объекта
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    ///     Изображение
    /// </summary>
    public Uri Image { get; set; } = null!;

    /// <summary>
    ///     Лист регистрации
    /// </summary>
    public RegistrationSheet.RegistrationSheet RegistrationSheet { get; set; } = null!;

    /// <summary>
    ///     Учетный лист
    /// </summary>
    public RecordSheet.RecordSheet RecordSheet { get; set; } = null!;

    /// <summary>
    ///     Приказы
    /// </summary>
    public List<Uri> Orders { get; set; } = [];

    /// <summary>
    ///     Пользователи и их роли в этом проекте
    /// </summary>
    // todo: в dbo почему то не пишем ._. . ps в dto нет, чтобы другие не знали про роли
    // todo: нужно сделать поиск объектов, в духе: найди все объекты, где есть userId в ConstructionSiteUserRoles,
    // чтобы отображать на глвной странице
    public List<ConstructionSiteUserRole> ConstructionSiteUserRoles { get; set; } = [];
}