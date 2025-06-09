using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.Notifications;

public class NotificationSearchRequest : SearchRequestBase
{
    public Guid? UserId { get; set; }

    public bool? IsRead { get; set; }
}