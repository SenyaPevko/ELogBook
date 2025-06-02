using Domain.AccessChecker;
using Domain.Entities.RegistrationSheet;
using Domain.Permissions.Base;
using Domain.Permissions.ConstructionSite;
using Domain.Repository;
using Domain.RequestArgs.RegistrationSheetItems;
using Infrastructure.Permissions.Base;

namespace Infrastructure.Permissions.ConstructionSitePermissionServices;

public class RegistrationSheetItemPermissionService(
    IAccessChecker<RegistrationSheetItem, RegistrationSheetItemUpdateArgs> accessChecker,
    IRepository<RegistrationSheetItem> repository)
    : EntityPermissionServiceBase<RegistrationSheetItem, RegistrationSheetItemUpdateArgs,
        RegistrationSheetItemPermission>(accessChecker, repository)
{
}