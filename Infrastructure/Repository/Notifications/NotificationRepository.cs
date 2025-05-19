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

public class NotificationRepository(
    IStorage<Notification, NotificationSearchRequest> storage,
    IHubContext<NotificationHub> hubContext,
    IConnectionManager connectionManager)
    : RepositoryBase<Notification, InvalidNotificationReason, NotificationSearchRequest>(storage)
{
    protected override async Task ValidateCreationAsync(Notification entity,
        IWriteContext<InvalidNotificationReason> writeContext, CancellationToken cancellationToken)
    {
    }

    protected override async Task ValidateUpdateAsync(Notification oldEntity, Notification newEntity,
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

    protected override async Task AfterCreateAsync(Notification entity,
        IWriteContext<InvalidNotificationReason> writeContext, CancellationToken cancellationToken) =>
        await NotifyUser(entity, cancellationToken);

    protected override async Task AfterBulkCreateAsync(List<Notification> entities,
        IBulkWriteContext<Notification, InvalidNotificationReason> writeContext, CancellationToken cancellationToken) =>
        await entities.SelectAsync(e => NotifyUser(e, cancellationToken));


    private async Task NotifyUser(Notification entity, CancellationToken cancellationToken)
    {
        var connections = connectionManager.GetConnections(entity.UserId).ToList();
        if (connections.Count != 0)
            await hubContext.Clients.Clients(connections).SendAsync(
                SignalrSettings.ClientMethods.ReceiveNotification, entity.ToDto(),
                cancellationToken: cancellationToken);
    }
}