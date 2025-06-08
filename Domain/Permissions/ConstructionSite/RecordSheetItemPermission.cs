using Domain.Permissions.Base;

namespace Domain.Permissions.ConstructionSite;

public class RecordSheetItemPermission : EntityPermissionsBase
{
    public bool CanUpdateDeviations { get; set; }
    public bool CanUpdateDirections { get; set; }
    public bool CanUpdateRepresentativeId { get; set; }
    public bool CanUpdateComplianceNoteUserId { get; set; }
}