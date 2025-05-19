namespace Domain.Dtos.Notifications;

public class NotificationDto
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public Guid? RelatedEntityId { get; set; }
}