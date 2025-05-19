using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.Organizations;

public class OrganizationSearchRequest : SearchRequestBase
{
    public string? Name { get; set; }
}