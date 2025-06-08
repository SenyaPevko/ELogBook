using Domain.AccessChecker;
using Domain.Entities.RecordSheet;
using Domain.Permissions.ConstructionSite;
using Domain.RequestArgs.Base;
using Domain.RequestArgs.RecordSheetItems;

namespace Infrastructure.Permissions.ConstructionSitePermissionServices;

public class RecordSheetItemPermissionService(IRecordSheetItemAccessChecker accessChecker)
    : EntityUnderConstructionSitePermissionServiceBase<RecordSheetItem, RecordSheetItemUpdateArgs,
        RecordSheetItemPermission>(accessChecker, accessChecker)
{
    protected override async Task FillPermissions(Guid? constructionSiteId, RecordSheetItemPermission permissions)
    {
        if (constructionSiteId is null)
            return;

        var userRoles = await accessChecker.GetUserRoleTypes(constructionSiteId.Value);

        permissions.CanUpdateDeviations = accessChecker.CanUpdateDeviations(userRoles);
        permissions.CanUpdateDirections = accessChecker.CanUpdateDirections(userRoles);
        permissions.CanUpdateRepresentativeId = accessChecker.CanUpdateRepresentativeId(userRoles);
        permissions.CanUpdateComplianceNoteUserId = accessChecker.CanUpdateComplianceNoteUserId(userRoles);
    }

    protected override RecordSheetItemUpdateArgs FillUpdateArgs() =>
        new()
        {
            Deviations = string.Empty,
            DeviationFilesIds = new ListUpdateOfId<string>
            {
                Add = [string.Empty],
                Remove = [string.Empty],
            },
            Directions = string.Empty,
            DirectionFilesIds = new ListUpdateOfId<string>
            {
                Add = [string.Empty],
                Remove = [string.Empty],
            },
            RepresentativeId = Guid.Empty,
            ComplianceNoteUserId = Guid.Empty,
        };
}