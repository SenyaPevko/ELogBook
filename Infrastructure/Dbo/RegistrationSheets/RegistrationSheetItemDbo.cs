using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Dbo.RegistrationSheets;

/// <summary>
///     Ряд регистрационного листа
/// </summary>
[Table("registrationSheetItems")]
public class RegistrationSheetItemDbo : EntityDbo
{
    /// <summary>
    ///     Id организации
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    ///     Идентификатор пользователя, создавшего ряд
    /// </summary>
    public Guid CreatorId { get; set; }

    /// <summary>
    ///     Дата приезда
    /// </summary>
    public DateTime ArrivalDate { get; set; }

    /// <summary>
    ///     Дата отъезда
    /// </summary>
    public DateTime DepartureDate { get; set; }

    public Guid RegistrationSheetId { get; set; }
}