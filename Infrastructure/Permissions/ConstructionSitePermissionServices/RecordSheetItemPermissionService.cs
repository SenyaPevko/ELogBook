using Domain.AccessChecker;
using Domain.Entities.RecordSheet;
using Domain.Permissions.ConstructionSite;
using Domain.Repository;
using Domain.RequestArgs.RecordSheetItems;
using Infrastructure.Permissions.Base;

namespace Infrastructure.Permissions.ConstructionSitePermissionServices;

public class RecordSheetItemPermissionService(
    IAccessChecker<RecordSheetItem, RecordSheetItemUpdateArgs> accessChecker,
    IRepository<RecordSheetItem> repository)
    : EntityPermissionServiceBase<RecordSheetItem, RecordSheetItemUpdateArgs,
        RecordSheetItemPermission>(accessChecker, repository)
{
    protected override async Task FillPermissions(Guid? entityId, RecordSheetItemPermission permissions, CancellationToken cancellationToken)
    {
        if (entityId is null)
            return;
        
        var entity = await Repository.GetByIdAsync(entityId.Value, cancellationToken);
        if (entity is null)
            return;
        
        /*
        permissions.CanUpdateOrders = await accessChecker.CanUpdateOrders(entity);*/
    }
}