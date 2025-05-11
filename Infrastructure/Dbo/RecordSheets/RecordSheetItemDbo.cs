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