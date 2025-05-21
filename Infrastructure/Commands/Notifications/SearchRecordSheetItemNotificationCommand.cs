using Domain.AccessChecker;
using Domain.Dtos.Notifications;
using Domain.Entities.Notifications;
using Domain.Repository;
using Domain.RequestArgs.Notifications;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Notifications;

public class SearchRecordSheetItemNotificationCommand(
    IRepository<RecordSheetItemNotification, InvalidNotificationReason, NotificationSearchRequest> repository, IAccessChecker<RecordSheetItemNotification> accessChecker)
    : SearchCommandBase<RecordSheetItemNotificationDto, RecordSheetItemNotification, InvalidNotificationReason,
        NotificationSearchRequest>(repository, accessChecker)
{
    protected override async Task<RecordSheetItemNotificationDto> MapToDtoAsync(RecordSheetItemNotification entity)
    {
        return await entity.ToDto();
    }
}