namespace Domain.Dtos.WorkIssue;

public class WorkIssueDto : EntityDto
{
    public required List<WorkIssueItemDto> Items { get; set; }
}