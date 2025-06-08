using Domain.Entities.RecordSheet;
using Domain.Entities.Roles;
using Domain.RequestArgs.RecordSheetItems;

namespace Domain.AccessChecker;

public interface IRecordSheetItemAccessChecker
    : IAccessChecker<RecordSheetItem, RecordSheetItemUpdateArgs>,
        IEntityUnderConstructionSiteAccessChecker<RecordSheetItemUpdateArgs>
{
    bool CanUpdateDeviations(List<ConstructionSiteUserRoleType> userRoles);
    bool CanUpdateDirections(List<ConstructionSiteUserRoleType> userRoles);
    bool CanUpdateRepresentativeId(List<ConstructionSiteUserRoleType> userRoles);
    bool CanUpdateComplianceNoteUserId(List<ConstructionSiteUserRoleType> userRoles);
}