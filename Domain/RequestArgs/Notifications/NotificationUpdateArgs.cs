using Domain.RequestArgs.Base;

namespace Domain.RequestArgs.Notifications;

public class NotificationUpdateArgs : EntityUpdateArgs
{
    public bool? IsRead { get; set; }
}