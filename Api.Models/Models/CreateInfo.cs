namespace Api.Models.Models;

public class CreateInfo
{
    /// <summary>
    ///     Дата-время создания сущности
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    ///     Идентификатор пользователя создавшего сущность
    /// </summary>
    public string? CreatedByUserId { get; set; }

    /// <summary>
    ///     Наименование подсистемы, с использованием которой была создана сущность
    /// </summary>
    public string CreatedWith { get; set; } = null!;

    public override string ToString() => $"{nameof(CreatedAt)}: {CreatedAt}, {nameof(CreatedByUserId)}: {CreatedByUserId}, nameof(CreatedWith): CreatedWith";
}