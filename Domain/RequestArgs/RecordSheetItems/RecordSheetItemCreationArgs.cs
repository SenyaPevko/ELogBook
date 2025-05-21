using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.RecordSheetItems;

public class RecordSheetItemCreationArgs : EntityCreationArgs
{
    /// <summary>
    ///     Выявленные отступления ...
    /// </summary>
    public required string Deviations { get; set; }

    public List<string> DeviationFilesIds { get; set; } = [];

    /// <summary>
    ///     Указания об устранении отступлений ...
    /// </summary>
    public required string Directions { get; set; }

    public List<string> DirectionFilesIds { get; set; } = [];

    public required Guid RecordSheetId { get; set; }
}