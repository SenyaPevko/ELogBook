namespace Domain.Dtos.Notifications;

public class RecordSheetItemNotificationDto : EntityDto
{
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public Guid? RecordSheetItemId { get; set; }
}