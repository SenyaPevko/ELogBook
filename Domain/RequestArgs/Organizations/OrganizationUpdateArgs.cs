using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.Organizations;

public class OrganizationUpdateArgs : EntityUpdateArgs
{
    public string? Name { get; set; }
    public ListUpdateOfId<Guid>? UserIds { get; set; }
}