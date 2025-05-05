namespace Domain.RequestArgs.UpdateArgs;

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
}