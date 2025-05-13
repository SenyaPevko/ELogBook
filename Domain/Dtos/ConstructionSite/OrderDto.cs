using Domain.Entities.Base;
using FileInfo = Domain.FileStorage.FileInfo;

namespace Domain.Dtos.ConstructionSite;

public class OrderDto : IItemWithId
{
    public FileInfo File { get; set; } = null!;
    public Guid UserInChargeId { get; set; }
    public Guid Id { get; set; }
}