using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.RegistrationSheetItems;

public class RegistrationSheetItemUpdateArgs : EntityUpdateArgs
{
    /// <summary>
    ///     Дата приезда
    /// </summary>
    public DateTime? ArrivalDate { get; set; }

    /// <summary>
    ///     Дата отъезда
    /// </summary>
    public DateTime? DepartureDate { get; set; }
}