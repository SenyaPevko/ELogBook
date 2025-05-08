using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.Organizations;

public class OrganizationCreationArgs : EntityCreationArgs
{
    public required string Name { get; set; }
}