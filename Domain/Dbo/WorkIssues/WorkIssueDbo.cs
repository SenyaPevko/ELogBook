namespace Domain.Dbo.WorkIssues;

/// <summary>
/// Рабочие вопросы
/// </summary>
public class WorkIssueDbo : UpdatableDomainEntityDbo
{
    public List<Guid> WorkIssueItemIds { get; set; } = [];
}