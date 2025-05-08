using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.RegistrationSheetItems;

public class RegistrationSheetItemCreationArgs : EntityCreationArgs
{
    /// <summary>
    ///     Дата приезда
    /// </summary>
    public required DateTime ArrivalDate { get; set; }

    /// <summary>
    ///     Дата отъезда
    /// </summary>
    public required DateTime DepartureDate { get; set; }

    public required Guid CreatorId { get; set; }

    public required Guid RegistrationSheetId { get; set; }
}