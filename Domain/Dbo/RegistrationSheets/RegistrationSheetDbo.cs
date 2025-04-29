namespace Domain.Dbo.RegistrationSheets;

/// <summary>
/// Регистрационный лист
/// </summary>
public class RegistrationSheetDbo : EntityDbo
{
    /// <summary>
    /// Идентификаторы рядов в регистрационном листе
    /// </summary>
    public List<Guid> RegistrationSheetItemIds { get; set; } = [];
}