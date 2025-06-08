using Domain.AccessChecker;
using Domain.Entities.RegistrationSheet;
using Domain.Permissions.ConstructionSite;
using Domain.Repository;
using Domain.RequestArgs.RegistrationSheetItems;

namespace Infrastructure.Permissions.ConstructionSitePermissionServices;

public class RegistrationSheetItemPermissionService(
    IRegistrationSheetItemAccessChecker accessChecker,
    IRepository<RegistrationSheetItem> repository)
    : EntityUnderConstructionSitePermissionServiceBase<RegistrationSheetItem, RegistrationSheetItemUpdateArgs,
        RegistrationSheetItemPermission>(accessChecker, accessChecker)
{
    protected override RegistrationSheetItemUpdateArgs FillUpdateArgs() =>
        new()
        {
            ArrivalDate = DateTime.Now,
            DepartureDate = DateTime.Now
        };
}