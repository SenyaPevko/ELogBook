namespace Domain.Dbo.RecordSheets;

/// <summary>
/// Учетный лист
/// </summary>
public class RecordSheetDbo : EntityDbo
{
    /// <summary>
    /// Номер листа
    /// </summary>
    public int Number {get; set;} 
    
    /// <summary>
    /// Идентификаторы рядов учетного листа
    /// </summary>
    public List<Guid> RecordSheetItemIds { get; set; } = [];
}