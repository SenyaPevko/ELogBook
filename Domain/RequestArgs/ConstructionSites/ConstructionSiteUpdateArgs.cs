using Domain.Entities.ConstructionSite;
using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

/// <summary>
/// Обновление таких полей как RegistrationSheet, RecordSheet, WorkIssue происходит путем обновления вложенных в них
/// сущностей через соответствующие апи
/// </summary>
public class ConstructionSiteUpdateArgs : EntityUpdateArgs
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

    public ListUpdate<OrderCreationArgs>? Orders { get; set; }
    
    public ListUpdate<ConstructionSiteUserRoleCreationArgs, ConstructionSiteUserRoleUpdateArgs>? UserRoles { get; set; }
}