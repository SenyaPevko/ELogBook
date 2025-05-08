using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.RegistrationSheetItems;

public class RegistrationSheetItemCreationArgs : EntityCreationArgs
{
    /// <summary>
    ///     Дата приезда
    /// </summary>
    public DateTime ArrivalDate { get; set; }

    /// <summary>
    ///     Дата отъезда
    /// </summary>
    public DateTime DepartureDate { get; set; }

    public Guid CreatorId { get; set; }

    public Guid RegistrationSheetId { get; set; }
}