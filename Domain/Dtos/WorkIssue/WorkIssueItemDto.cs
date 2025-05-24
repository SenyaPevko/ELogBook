namespace Domain.Dtos.WorkIssue;

public class WorkIssueItemDto : EntityDto
{
    /// <summary>
    ///     Вопрос
    /// </summary>
    public string Question { get; set; } = null!;
    
    /// <summary>
    /// Пользователь задавший вопрос
    /// </summary>
    public UserDto QuestionedBy { get; set; } = null!;

    /// <summary>
    ///     Дата вопроса
    /// </summary>
    public DateTime QuestionDate { get; set; }

    /// <summary>
    ///     Ответ
    /// </summary>
    public string Answer { get; set; } = null!;
    
    /// <summary>
    /// Пользователь давший ответ
    /// </summary>
    public UserDto? AnsweredBy { get; set; } = null!;

    public Guid? AnswerUserId { get; set; }

    /// <summary>
    ///     Дата ответа
    /// </summary>
    public DateTime AnswerDate { get; set; }
}