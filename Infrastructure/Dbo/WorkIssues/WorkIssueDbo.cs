using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Dbo.WorkIssues;

/// <summary>
///     Рабочие вопросы
/// </summary>
[Table("workIssues")]
public class WorkIssueDbo : EntityDbo
{
    public List<Guid> WorkIssueItemIds { get; set; } = [];
}