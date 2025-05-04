namespace Domain.Entities.Base;

public class UpdateInfo : CreateInfo
{
    public DateTimeOffset UpdatedAt { get; set; }

    public Guid? UpdatedByUserId { get; set; }
}