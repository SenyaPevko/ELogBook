using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Dbo.WorkIssues;

/// <summary>
///     Ряд таблицы "рабочие вопросы"
/// </summary>
[Table("workIssueItems")]
public class WorkIssueItemDbo : EntityDbo
{
    /// <summary>
    ///     Вопрос
    /// </summary>
    public  string Question { get; set; } = null!;

    /// <summary>
    ///     Дата вопроса
    /// </summary>
    public  DateTime QuestionDate { get; set; }

    /// <summary>
    ///     Ответ
    /// </summary>
    public  string Answer { get; set; } = null!;

    /// <summary>
    ///     Дата ответа
    /// </summary>
    public  DateTime AnswerDate { get; set; }
    
    public Guid WorkIssueId { get; set; }
}