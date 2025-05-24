using Domain.Entities.Base;
using Domain.Entities.Users;

namespace Domain.Entities.WorkIssues;

public class WorkIssueItem : EntityInfo
{
    /// <summary>
    ///     Вопрос
    /// </summary>
    public string Question { get; set; } = null!;
    
    /// <summary>
    /// Пользователь задавший вопрос
    /// </summary>
    public User QuestionedBy { get; set; } = null!;

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
    public User? AnsweredBy { get; set; } = null!;
    
    /// <summary>
    ///     Дата ответа
    /// </summary>
    public DateTime AnswerDate { get; set; }

    public Guid? AnswerUserId { get; set; }

    public Guid WorkIssueId { get; set; }
}