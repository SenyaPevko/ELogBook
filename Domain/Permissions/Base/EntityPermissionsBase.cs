namespace Domain.Permissions.Base;

public abstract class EntityPermissionsBase
{
    public bool CanRead { get; set; }
    public bool CanCreate { get; set; }
    public bool CanUpdate { get; set; }
}