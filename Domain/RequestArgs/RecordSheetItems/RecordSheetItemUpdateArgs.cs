using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.RecordSheetItems;

public class RecordSheetItemUpdateArgs : EntityUpdateArgs
{
    /// <summary>
    ///     Выявленные отступления ...
    /// </summary>
    public string? Deviations { get; set; }

    /// <summary>
    ///     Указания об устранении отступлений ...
    /// </summary>
    public string? Directions { get; set; }

    /// <summary>
    ///     Id представителя ознакомленного с записью ...
    /// </summary>
    public Guid? RepresentativeId { get; set; }

    /// <summary>
    ///     Id пользователя оставившего отметку о выполнении указаний ...
    /// </summary>
    public Guid? ComplianceNoteUserId { get; set; }
}