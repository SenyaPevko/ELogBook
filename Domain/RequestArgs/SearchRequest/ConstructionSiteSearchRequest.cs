namespace Domain.RequestArgs.SearchRequest;

public class ConstructionSiteSearchRequest : SearchRequestBase
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public Guid? UserRoleUserId { get; set; }
}