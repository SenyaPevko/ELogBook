using FileInfo = Domain.FileStorage.FileInfo;

namespace Domain.Dtos.RecordSheet;

public class RecordSheetItemDto : EntityDto
{
    /// <summary>
    ///     Дата записи
    /// </summary>
    public required DateTime Date { get; set; }

    /// <summary>
    ///     Выявленные отступления ...
    /// </summary>
    public required string Deviations { get; set; }
    
    public List<FileInfo> DeviationFiles { get; set; } = [];

    /// <summary>
    ///     Указания об устранении отступлений ...
    /// </summary>
    public required string Directions { get; set; }
    
    public List<FileInfo> DirectionFiles { get; set; } = [];

    /// <summary>
    ///     Подпись специалиста ...
    /// </summary>
    public required string SpecialistSignature { get; set; }

    /// <summary>
    ///     С записью ознакомлен представитель ...
    /// </summary>
    public string? RepresentativeSignature { get; set; }

    /// <summary>
    ///     Отметка о выполнении указаний ...
    /// </summary>
    public string? ComplianceNote { get; set; }
}