using Core.Helpers;
using Domain.Entities.Notifications;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.Notifications;
using Domain.Settings;
using Domain.SignalR;
using Domain.Storage;
using Infrastructure.Commands;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Repository.Notifications;

// todo: когда будут добавляться новые типы уведомлений, то логику этого репозитория и соответствующего стореджа
// нужно будет сводить к базовому абстрактному классу и для уведомлений создавать наследники
public class RecordSheetItemNotificationRepository(
    IStorage<RecordSheetItemNotification, NotificationSearchRequest> storage,
    IHubContext<NotificationHub> hubContext,
    IConnectionManager connectionManager)
    : RepositoryBase<RecordSheetItemNotification, InvalidNotificationReason, NotificationSearchRequest>(storage)
{
    protected override async Task ValidateCreationAsync(RecordSheetItemNotification entity,
        IWriteContext<InvalidNotificationReason> writeContext, CancellationToken cancellationToken)
    {
    }

    protected override async Task ValidateUpdateAsync(RecordSheetItemNotification oldEntity, RecordSheetItemNotification newEntity,
        IWriteContext<InvalidNotificationReason> writeContext,
        CancellationToken cancellationToken)
    {
        if (oldEntity.IsRead is true && newEntity.IsRead is false)
            writeContext.AddInvalidData(new ErrorDetail<InvalidNotificationReason>
            {
                Path = nameof(oldEntity.IsRead),
                Reason = InvalidNotificationReason.UnreadNotificationIsFobidden,
                Value = nameof(oldEntity.IsRead)
            });
    }

    protected override async Task AfterCreateAsync(RecordSheetItemNotification entity,
        IWriteContext<InvalidNotificationReason> writeContext, CancellationToken cancellationToken) =>
        await NotifyUser(entity, cancellationToken);

    protected override async Task AfterBulkCreateAsync(List<RecordSheetItemNotification> entities,
        IBulkWriteContext<RecordSheetItemNotification, InvalidNotificationReason> writeContext, CancellationToken cancellationToken) =>
        await entities.SelectAsync(e => NotifyUser(e, cancellationToken));


    private async Task NotifyUser(RecordSheetItemNotification entity, CancellationToken cancellationToken)
    {
        var connections = connectionManager.GetConnections(entity.UserId).ToList();
        if (connections.Count != 0)
            await hubContext.Clients.Clients(connections).SendAsync(
                SignalrSettings.ClientMethods.ReceiveNotification, 
                await entity.ToDto(),
                cancellationToken: cancellationToken);
    }
}