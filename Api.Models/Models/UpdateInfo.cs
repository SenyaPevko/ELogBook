namespace Api.Models.Models;

public class UpdateInfo : CreateInfo
{
    /// <summary>
    ///     Дата-время последнего обновления сущности
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    ///     Идентификатор пользоватетя, который внес последние изменения
    /// </summary>
    public string? UpdatedByUserId { get; set; }

    /// <summary>
    ///     Наименование подсистемы с помощью, которой было произведено последнее обновление 
    /// </summary>
    public string UpdatedWith { get; set; } = null!;
}