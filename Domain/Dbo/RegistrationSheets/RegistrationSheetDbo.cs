namespace Domain.Dbo.RegistrationSheets;

/// <summary>
/// Регистрационный лист
/// </summary>
public class RegistrationSheetDbo : UpdatableDomainEntityDbo
{
    /// <summary>
    /// Идентификаторы рядов в регистрационном листе
    /// </summary>
    public List<Guid> RegistrationSheetItemIds { get; set; } = [];
}