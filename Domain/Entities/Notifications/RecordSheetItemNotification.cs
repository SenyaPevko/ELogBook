using Domain.Entities.Base;

namespace Domain.Entities.Notifications;

public class RecordSheetItemNotification : EntityInfo
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; }
    public Guid RecordSheetItemId { get; set; }
    public NotificationType NotificationType => NotificationType.RecordSheetItemCreationNotification;
    public Guid RecordSheetId { get; set; }
    public Guid ConstructionSiteId { get; set; }
}