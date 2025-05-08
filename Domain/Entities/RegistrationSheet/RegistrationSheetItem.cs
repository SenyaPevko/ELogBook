using Domain.Entities.Base;

namespace Domain.Entities.RegistrationSheet;

public class RegistrationSheetItem : EntityInfo
{
    public string OrganizationName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    /// <summary>
    ///     Дата приезда
    /// </summary>
    public DateTime ArrivalDate { get; set; }

    /// <summary>
    ///     Дата отъезда
    /// </summary>
    public DateTime DepartureDate { get; set; }

    public string Signature { get; set; } = null!;
}