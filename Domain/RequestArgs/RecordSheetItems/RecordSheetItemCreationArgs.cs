using Domain.RequestArgs.Base;
using MongoDB.Bson;

namespace Domain.RequestArgs.RecordSheetItems;

public class RecordSheetItemCreationArgs : EntityCreationArgs
{
    /// <summary>
    ///     Выявленные отступления ...
    /// </summary>
    public required string Deviations { get; set; }
    
    public List<ObjectId>? DeviationFilesIds { get; set; } = [];

    /// <summary>
    ///     Указания об устранении отступлений ...
    /// </summary>
    public required string Directions { get; set; }
    
    public List<ObjectId> DirectionFilesIds { get; set; } = [];

    public required Guid RecordSheetId { get; set; }
}