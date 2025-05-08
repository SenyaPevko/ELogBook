using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

public class ConstructionSiteCreationArgs : EntityCreationArgs
{
    /// <summary>
    ///     Название объекта
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Название объекта
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    ///     Название объекта
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    ///     Изображение
    /// </summary>
    public required Uri Image { get; set; }
}