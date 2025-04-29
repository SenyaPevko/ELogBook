namespace Api.Models.Models.RecordSheet;

public class RecordSheetItem : EntityInfo
{
    /// <summary>
    /// Дата записи
    /// </summary>
    public required DateTime Date { get; set; }
    
    /// <summary>
    /// Выявленные отступления ...
    /// </summary>
    public required string Deviations { get; set; }
    
    /// <summary>
    /// Указания об устранении отступлений ...
    /// </summary>
    public required string Directions { get; set; }
    
    /// <summary>
    /// Подпись специалиста ...
    /// </summary>
    public required string SpecialistSignature { get; set; }
    
    /// <summary>
    /// С записью ознакомлен представитель ...
    /// </summary>
    public required string RepresentativeSignature { get; set; }
    
    /// <summary>
    /// Отметка о выполнении указаний ...
    /// </summary>
    public required string ComplianceNote { get; set; }
}