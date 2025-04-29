using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Dbo;

public class  UpdatableDomainEntityDbo
{
    [BsonIgnore] private DateTimeOffset createdAt;
    [BsonIgnore] private DateTimeOffset updatedAt;

    [BsonId]
    public Guid Id { get; set; }
    
    public DateTimeOffset CreatedAt
    {
        get => createdAt.ToUniversalTime();
        set => createdAt = value.ToUniversalTime();
    }

    [BsonIgnore]
    public Guid CreatedAtTimestamp { get; set; }
    
    public string? CreatedByUserId { get; set; }
    
    public string CreatedWith { get; set; } = null!;
    
    public DateTimeOffset UpdatedAt
    {
        get => updatedAt.ToUniversalTime();
        set => updatedAt = value.ToUniversalTime();
    }

    [BsonIgnore]
    public Guid UpdatedAtTimestamp { get; set; }

    public string? UpdatedByUserId { get; set; }
    
    public string UpdatedWith { get; set; } = null!;
}