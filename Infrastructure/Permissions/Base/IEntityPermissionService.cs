using Domain.Permissions.Base;

namespace Infrastructure.Permissions.Base;

public interface IEntityPermissionService<TPermission> where TPermission : EntityPermissionsBase, new()
{
    public Task<TPermission> GetUserPermissions(Guid? entityId, CancellationToken cancellationToken);
}