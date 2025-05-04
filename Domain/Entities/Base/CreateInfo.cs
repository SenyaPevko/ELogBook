namespace Domain.Entities.Base;

public class CreateInfo
{
    public DateTimeOffset CreatedAt { get; set; }

    public Guid? CreatedByUserId { get; set; }
}