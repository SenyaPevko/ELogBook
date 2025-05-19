using Domain.Entities.Notifications;
using Domain.RequestArgs.Notifications;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.Notifications;

public class NotificationAccessChecker(IRequestContext context) : AccessCheckerBase<Notification, NotificationUpdateArgs>(context)
{
    public override async Task<bool> CanUpdate(Notification entity)
    {
        return entity.UserId == Context.Auth.UserId;
    }
}