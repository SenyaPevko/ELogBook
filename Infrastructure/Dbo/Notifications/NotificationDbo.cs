using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Notifications;

namespace Infrastructure.Dbo.Notifications;

[Table("notifications")]
public class NotificationDbo : EntityDbo
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public Guid? RelatedEntityId { get; set; }
    public NotificationType NotificationType { get; set; }
}