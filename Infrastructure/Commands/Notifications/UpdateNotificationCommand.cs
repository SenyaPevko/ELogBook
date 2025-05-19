using Domain.AccessChecker;
using Domain.Dtos.Notifications;
using Domain.Entities.Notifications;
using Domain.Repository;
using Domain.RequestArgs.Notifications;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Notifications;

public class UpdateNotificationCommand(
    IRepository<Notification, InvalidNotificationReason> repository,
    IAccessChecker<Notification, NotificationUpdateArgs> accessChecker)
    : UpdateCommandBase<RecordSheetItemNotificationDto, Notification,
        NotificationUpdateArgs, InvalidNotificationReason>(repository, accessChecker)
{
    protected override async Task<RecordSheetItemNotificationDto> MapToDtoAsync(Notification entity)
    {
        return await entity.ToDto();
    }

    protected override Task ApplyUpdatesAsync(Notification entity, NotificationUpdateArgs args)
    {
        if(args.IsRead is not null) entity.IsRead = args.IsRead.Value;
        
        return Task.CompletedTask;
    }
}