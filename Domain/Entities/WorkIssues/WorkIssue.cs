using Domain.Entities.Base;

namespace Domain.Entities.WorkIssues;

public class WorkIssue : EntityInfo
{
    public List<WorkIssueItem> Items { get; set; } = [];

    public Guid ConstructionSiteId { get; set; }
}