using Domain.Entities.Base;

namespace Domain.Entities.WorkIssues;

public class WorkIssueItem : EntityInfo
{
    /// <summary>
    ///     Вопрос
    /// </summary>
    public string Question { get; set; } = null!;

    /// <summary>
    ///     Дата вопроса
    /// </summary>
    public DateTime QuestionDate { get; set; }

    /// <summary>
    ///     Ответ
    /// </summary>
    public string Answer { get; set; } = null!;

    /// <summary>
    ///     Дата ответа
    /// </summary>
    public DateTime AnswerDate { get; set; }

    public Guid WorkIssueId { get; set; }
}