using Domain.AccessChecker;
using Domain.Dtos.Notifications;
using Domain.Entities.Notifications;
using Domain.Repository;
using Domain.RequestArgs.Notifications;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Notifications;

public class SearchNotificationCommand(
    IRepository<Notification, InvalidNotificationReason, NotificationSearchRequest> repository, IAccessChecker<Notification> accessChecker)
    : SearchCommandBase<RecordSheetItemNotificationDto, Notification, InvalidNotificationReason,
        NotificationSearchRequest>(repository, accessChecker)
{
    protected override async Task<RecordSheetItemNotificationDto> MapToDtoAsync(Notification entity)
    {
        return await entity.ToDto();
    }
}