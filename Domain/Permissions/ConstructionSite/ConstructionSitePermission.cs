using Domain.Permissions.Base;

namespace Domain.Permissions.ConstructionSite;

public class ConstructionSitePermission : EntityPermissionsBase
{
    public bool CanUpdateOrders { get; set; }
    
    public RegistrationSheetItemPermission RegistrationSheetItemPermission { get; set; } = null!;
    
    public RecordSheetItemPermission RecordSheetItemPermission { get; set; } = null!;
    
    public WorkIssueItemPermission WorkIssueItemPermission { get; set; } = null!;
}