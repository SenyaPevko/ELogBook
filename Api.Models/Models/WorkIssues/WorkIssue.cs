namespace Api.Models.Models.WorkIssues;

public class WorkIssue : EntityInfo
{
    public List<WorkIssueItem> Items { get; set; } = [];
}