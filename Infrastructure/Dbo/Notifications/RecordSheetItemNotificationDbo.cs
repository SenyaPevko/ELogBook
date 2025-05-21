using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Notifications;

namespace Infrastructure.Dbo.Notifications;

[Table("notifications")]
public class RecordSheetItemNotificationDbo : EntityDbo
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; }
    public Guid RecordSheetItemId { get; set; }
    public NotificationType NotificationType { get; set; }
    public Guid RecordSheetId { get; set; }
    public Guid ConstructionSiteId { get; set; }
}