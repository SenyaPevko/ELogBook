using Domain.Entities.Base;

namespace Domain.Entities.RegistrationSheet;

public class RegistrationSheetItem : EntityInfo
{
    public required string OrganizationName { get; set; }

    public required string Name { get; set; }

    public required string Surname { get; set; }

    public required string Patronymic { get; set; }

    /// <summary>
    ///     Дата приезда
    /// </summary>
    public required DateTime ArrivalDate { get; set; }

    /// <summary>
    ///     Дата отъезда
    /// </summary>
    public required DateTime DepartureDate { get; set; }

    public required string Signature { get; set; }
}