using Domain.Entities.Base;

namespace Domain.Entities.RegistrationSheet;

public class RegistrationSheet : EntityInfo
{
    public List<RegistrationSheetItem> Items { get; set; } = [];
}