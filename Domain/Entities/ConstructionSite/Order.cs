using Domain.Entities.Base;
using MongoDB.Bson;

namespace Domain.Entities.ConstructionSite;

public class Order : IItemWithId
{
    public ObjectId FileId { get; set; }
    public Guid UserInChargeId { get; set; }
    public Guid Id { get; set; }
}