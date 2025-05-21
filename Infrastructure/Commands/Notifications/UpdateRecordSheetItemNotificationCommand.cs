using Domain.AccessChecker;
using Domain.Dtos.Notifications;
using Domain.Entities.Notifications;
using Domain.Repository;
using Domain.RequestArgs.Notifications;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Notifications;

public class UpdateRecordSheetItemNotificationCommand(
    IRepository<RecordSheetItemNotification, InvalidNotificationReason> repository,
    IAccessChecker<RecordSheetItemNotification, NotificationUpdateArgs> accessChecker)
    : UpdateCommandBase<RecordSheetItemNotificationDto, RecordSheetItemNotification,
        NotificationUpdateArgs, InvalidNotificationReason>(repository, accessChecker)
{
    protected override async Task<RecordSheetItemNotificationDto> MapToDtoAsync(RecordSheetItemNotification entity)
    {
        return await entity.ToDto();
    }

    protected override Task ApplyUpdatesAsync(RecordSheetItemNotification entity, NotificationUpdateArgs args)
    {
        if (args.IsRead is not null) entity.IsRead = args.IsRead.Value;

        return Task.CompletedTask;
    }
}