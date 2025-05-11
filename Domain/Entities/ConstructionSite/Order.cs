using Domain.Entities.Base;

namespace Domain.Entities.ConstructionSite;

public class Order : IItemWithId
{
    public Guid Id { get; set; }

    public string Link { get; set; } = null!;
    
    public Guid UserInChargeId { get; set; }
}