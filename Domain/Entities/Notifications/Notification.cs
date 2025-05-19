using Domain.Entities.Base;

namespace Domain.Entities.Notifications;

public class Notification : EntityInfo
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public Guid? RelatedEntityId { get; set; }
    public NotificationType NotificationType { get; set; }
}