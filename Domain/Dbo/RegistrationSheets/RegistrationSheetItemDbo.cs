namespace Domain.Dbo.RegistrationSheets;

/// <summary>
/// Ряд регистрационного листа
/// </summary>
public class RegistrationSheetItemDbo : EntityDbo
{
    /// <summary>
    /// Id организации
    /// </summary>
    public required Guid OrganizationId {get; set;}
    
    /// <summary>
    /// Идентификатор пользователя, создавшего ряд
    /// </summary>
    public required Guid CreatorId {get; set;}
    
    /// <summary>
    /// Дата приезда
    /// </summary>
    public required DateTime ArrivalDate { get; set; }
    
    /// <summary>
    /// Дата отъезда
    /// </summary>
    public required DateTime DepartureDate { get; set; }
}