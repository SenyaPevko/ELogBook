using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Dbo.RecordSheets;

/// <summary>
///     Ряд учетного листа
/// </summary>
[Table("recordSheetItems")]
public class RecordSheetItemDbo : EntityDbo
{
    /// <summary>
    ///     Дата записи
    /// </summary>
    public required DateTime Date { get; set; }

    /// <summary>
    ///     Выявленные отступления ...
    /// </summary>
    public required string Deviations { get; set; }

    /// <summary>
    ///     Указания об устранении отступлений ...
    /// </summary>
    public required string Directions { get; set; }

    /// <summary>
    ///     Id специалиста поставившего подпись ...
    /// </summary>
    public required Guid SpecialistId { get; set; }

    /// <summary>
    ///     Id представителя ознакомленного с записью ...
    /// </summary>
    public required Guid RepresentativeId { get; set; }

    /// <summary>
    ///     Id пользователя оставившего отметку о выполнении указаний ...
    /// </summary>
    public required Guid ComplianceNoteUserId { get; set; }
}