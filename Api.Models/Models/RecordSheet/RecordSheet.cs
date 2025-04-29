namespace Api.Models.Models.RecordSheet;

public class RecordSheet : EntityInfo
{
    public int Number { get; set; }
    
    public List<RecordSheetItem> Items { get; set; } = [];
}