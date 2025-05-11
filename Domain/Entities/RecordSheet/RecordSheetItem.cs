using Domain.Entities.Base;

namespace Domain.Entities.RecordSheet;

public class RecordSheetItem : EntityInfo
{
    /// <summary>
    ///     Дата записи
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     Выявленные отступления ...
    /// </summary>
    public string Deviations { get; set; } = null!;

    /// <summary>
    ///     Указания об устранении отступлений ...
    /// </summary>
    public string Directions { get; set; } = null!;

    /// <summary>
    ///     Подпись специалиста ...
    /// </summary>
    public string SpecialistSignature { get; set; } = null!;

    /// <summary>
    ///     С записью ознакомлен представитель ...
    /// </summary>
    public string? RepresentativeSignature { get; set; }

    /// <summary>
    ///     Отметка о выполнении указаний ...
    /// </summary>
    public string? ComplianceNoteSignature { get; set; }

    /// <summary>
    ///     Id специалиста поставившего подпись ...
    /// </summary>
    public Guid SpecialistId { get; set; }

    /// <summary>
    ///     Id представителя ознакомленного с записью ...
    /// </summary>
    public Guid? RepresentativeId { get; set; }

    /// <summary>
    ///     Id пользователя оставившего отметку о выполнении указаний ...
    /// </summary>
    public Guid? ComplianceNoteUserId { get; set; }

    public Guid RecordSheetId { get; set; }
}