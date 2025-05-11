namespace Domain.Dtos.RecordSheet;

public class RecordSheetDto : EntityDto
{
    public int Number { get; set; }

    public List<RecordSheetItemDto> Items { get; set; } = [];

    public Guid ConstructionSiteId { get; set; }
}