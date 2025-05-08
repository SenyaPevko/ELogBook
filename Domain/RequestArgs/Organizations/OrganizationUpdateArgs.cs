using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.Organizations;

public class OrganizationUpdateArgs : IEntityUpdateArgs
{
    public string? Name { get; set; }
    public IListUpdate<Guid>? UserIds { get; set; }
}