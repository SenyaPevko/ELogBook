using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Dbo.RecordSheets;

/// <summary>
///     Учетный лист
/// </summary>
[Table("recordSheets")]
public class RecordSheetDbo : EntityDbo
{
    /// <summary>
    ///     Номер листа
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    ///     Идентификаторы рядов учетного листа
    /// </summary>
    public List<Guid> RecordSheetItemIds { get; set; } = [];
    
    public Guid ConstructionSiteId { get; set; }
}