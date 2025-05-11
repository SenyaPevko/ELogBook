using Domain.Entities.Base;

namespace Domain.Entities.RecordSheet;

public class RecordSheet : EntityInfo
{
    public int Number { get; set; }

    public List<RecordSheetItem> Items { get; set; } = [];

    public Guid ConstructionSiteId { get; set; }
}