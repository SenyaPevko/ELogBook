namespace Domain.Dbo.WorkIssues;

/// <summary>
/// Ряд таблицы "рабочие вопросы"
/// </summary>
public class WorkIssueItemDbo : EntityDbo
{
    /// <summary>
    /// Вопрос
    /// </summary>
    public required string Question { get; set; }
    
    /// <summary>
    /// Дата вопроса
    /// </summary>
    public required DateTime QuestionDate { get; set; }
    
    /// <summary>
    /// Ответ
    /// </summary>
    public required string Answer { get; set; }
    
    /// <summary>
    /// Дата ответа
    /// </summary>
    public required DateTime AnswerDate { get; set; }
}