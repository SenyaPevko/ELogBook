using Domain.Permissions.Base;

namespace Domain.Permissions;

public class UserPermission : EntityPermissionsBase
{
    public bool CanUpdateOrganization { get; set; }
    public bool CanUpdateUserRole { get; set; }
}