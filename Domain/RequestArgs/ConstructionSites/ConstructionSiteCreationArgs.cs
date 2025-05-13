using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

public class ConstructionSiteCreationArgs : EntityCreationArgs
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
    ///     Генеральный подрядчик - Id организации
    /// </summary>
    public required Guid OrganizationId { get; set; }

    /// <summary>
    ///     Исполнители отдельных видов работ (субподрядчики) - наименование работ и название организации
    /// </summary>
    public required Guid SubOrganizationId { get; set; }

    /// <summary>
    ///     Список специалистов осуществляющих авторский надзор
    /// </summary>
    public required List<Guid> UserIds { get; set; }

    /// <summary>
    ///     Название объекта
    /// </summary>
    public required string Address { get; set; }
}