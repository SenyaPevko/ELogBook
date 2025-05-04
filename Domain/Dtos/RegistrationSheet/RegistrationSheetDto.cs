namespace Domain.Dtos.RegistrationSheet;

public class RegistrationSheetDto : EntityDto
{
    public List<RegistrationSheetItemDto> Items { get; set; } = [];
}