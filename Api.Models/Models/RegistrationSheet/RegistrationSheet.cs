namespace Api.Models.Models.RegistrationSheet;

public class RegistrationSheet : EntityInfo
{
    public List<RegistrationSheetItem> Items { get; set; } = [];
}