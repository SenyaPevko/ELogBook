using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

/// <summary>
///     Обновление таких полей как RegistrationSheet, RecordSheet, WorkIssue происходит путем обновления вложенных в них
///     сущностей через соответствующие апи
/// </summary>
public class ConstructionSiteUpdateArgs : EntityUpdateArgs
{
    /// <summary>
    ///     Краткое наименование объекта
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    ///     Наименование объекта в соответствии с документацией
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    ///     Название объекта
    /// </summary>
    public string? Address { get; set; }

    public ListUpdate<OrderCreationArgs>? Orders { get; set; }

    public ListUpdate<ConstructionSiteUserRoleCreationArgs, ConstructionSiteUserRoleUpdateArgs>? UserRoles { get; set; }

    public Guid? OrganizationId { get; set; }

    public Guid? SubOrganizationId { get; set; }
}