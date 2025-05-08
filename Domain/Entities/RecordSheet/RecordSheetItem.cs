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
    public  string Deviations { get; set; } = null!;

    /// <summary>
    ///     Указания об устранении отступлений ...
    /// </summary>
    public  string Directions { get; set; } = null!;

    /// <summary>
    ///     Подпись специалиста ...
    /// </summary>
    public  string SpecialistSignature { get; set; } = null!;

    /// <summary>
    ///     С записью ознакомлен представитель ...
    /// </summary>
    /// <summary>
    ///     Отметка о выполнении указаний ...
    /// </summary>
    public  string ComplianceNote { get; set; } = null!;
}