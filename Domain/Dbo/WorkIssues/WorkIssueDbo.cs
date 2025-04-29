namespace Domain.Dbo.WorkIssues;

/// <summary>
/// Рабочие вопросы
/// </summary>
public class WorkIssueDbo : EntityDbo
{
    public List<Guid> WorkIssueItemIds { get; set; } = [];
}