using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.WorkIssueItems;

public class WorkIssueItemUpdateArgs : EntityUpdateArgs
{
    /// <summary>
    ///     Вопрос
    /// </summary>
    public string? Question { get; set; }

    /// <summary>
    ///     Ответ
    /// </summary>
    public string? Answer { get; set; }
}