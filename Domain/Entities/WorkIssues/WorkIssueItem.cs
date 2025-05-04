using Domain.Entities.Base;

namespace Domain.Entities.WorkIssues;

public class WorkIssueItem : EntityInfo
{
    /// <summary>
    ///     Вопрос
    /// </summary>
    public required string Question { get; set; }

    /// <summary>
    ///     Дата вопроса
    /// </summary>
    public required DateTime QuestionDate { get; set; }

    /// <summary>
    ///     Ответ
    /// </summary>
    public required string Answer { get; set; }

    /// <summary>
    ///     Дата ответа
    /// </summary>
    public required DateTime AnswerDate { get; set; }
}