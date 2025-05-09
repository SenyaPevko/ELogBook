using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.ConstructionSites;

public class OrderCreationArgs : EntityCreationArgs
{
    public string Link { get; set; } = null!;
}