using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

public class OrderCreationArgs : EntityCreationArgs
{
    public required string FileId { get; set; }
    public required Guid UserInChargeId { get; set; }
}