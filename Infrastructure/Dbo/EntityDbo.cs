using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Dbo;

public abstract class EntityDbo
{
    [BsonIgnore] private DateTimeOffset _createdAt;
    [BsonIgnore] private DateTimeOffset _updatedAt;
    [BsonId] public Guid Id { get; set; }

    public DateTimeOffset CreatedAt
    {
        get => _createdAt.ToUniversalTime();
        set => _createdAt = value.ToUniversalTime();
    }

    public Guid? CreatedByUserId { get; set; }

    public DateTimeOffset UpdatedAt
    {
        get => _updatedAt.ToUniversalTime();
        set => _updatedAt = value.ToUniversalTime();
    }

    public Guid? UpdatedByUserId { get; set; }
}