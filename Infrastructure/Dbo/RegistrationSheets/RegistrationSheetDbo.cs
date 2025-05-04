using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Dbo.RegistrationSheets;

/// <summary>
///     Регистрационный лист
/// </summary>
[Table("registrationSheets")]
public class RegistrationSheetDbo : EntityDbo
{
    /// <summary>
    ///     Идентификаторы рядов в регистрационном листе
    /// </summary>
    public List<Guid> RegistrationSheetItemIds { get; set; } = [];
}