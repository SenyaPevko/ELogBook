namespace Domain.Dtos.WorkIssue;

public class WorkIssueItemDto : EntityDto
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

    public Guid? AnswerUserId { get; set; }

    /// <summary>
    ///     Дата ответа
    /// </summary>
    public DateTime AnswerDate { get; set; }
}