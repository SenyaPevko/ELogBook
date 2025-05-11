using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.WorkIssueItems;

public class WorkIssueItemCreationArgs : EntityCreationArgs
{
    /// <summary>
    ///     Вопрос
    /// </summary>
    public required string Question { get; set; }

    public required Guid WorkIssueId { get; set; }
}