using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

public class ConstructionSiteSearchRequest : SearchRequestBase
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public Guid? UserRoleUserId { get; set; }
    public Guid? RecordSheetId { get; set; }
    public Guid? RegistrationSheetId { get; set; }
    public Guid? WorkIssueId { get; set; }
}