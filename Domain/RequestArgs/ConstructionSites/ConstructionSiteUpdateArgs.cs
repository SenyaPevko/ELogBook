using Domain.Entities.ConstructionSite;
using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

/// <summary>
/// Обновление таких полей как RegistrationSheet, RecordSheet, WorkIssue происходит путем обновления вложенных в них
/// сущностей через соответствующие апи
/// </summary>
public class ConstructionSiteUpdateArgs : IEntityUpdateArgs
{
    /// <summary>
    ///     Название объекта
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Название объекта
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Название объекта
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    ///     Изображение
    /// </summary>
    public Uri? Image { get; set; }

    public IListUpdate<Order>? Orders { get; set; }
    
    public IListUpdate<ConstructionSiteUserRole>? UserRoles { get; set; }
}