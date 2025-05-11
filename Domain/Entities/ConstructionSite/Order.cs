using Domain.Entities.Base;

namespace Domain.Entities.ConstructionSite;

public class Order : IItemWithId
{
    public string Link { get; set; } = null!;

    public Guid UserInChargeId { get; set; }
    public Guid Id { get; set; }
}