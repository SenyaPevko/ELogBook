using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

public class OrderCreationArgs : EntityCreationArgs
{
    public required string Link { get; set; }
    public required Guid UserInChargeId { get; set; }
}