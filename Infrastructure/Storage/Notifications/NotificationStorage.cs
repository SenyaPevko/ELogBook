using Domain.Entities.Notifications;
using Domain.RequestArgs.Notifications;
using Infrastructure.Context;
using Infrastructure.Dbo.Notifications;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.Notifications;

public class NotificationStorage(AppDbContext dbContext, IRequestContext requestContext)
    : StorageBase<Notification, NotificationDbo, NotificationSearchRequest>(requestContext)
{
    protected override IMongoCollection<NotificationDbo> Collection => dbContext.Notifications;

    protected override Task MapEntityFromDboAsync(Notification entity, NotificationDbo dbo)
    {
        entity.Id = dbo.Id;
        entity.UserId = dbo.UserId;
        entity.Title = dbo.Title;
        entity.Message = dbo.Message;
        entity.IsRead = dbo.IsRead;
        entity.RelatedEntityId = dbo.RelatedEntityId;
        entity.NotificationType = dbo.NotificationType;
        entity.UpdateInfo = dbo.ToUpdateInfo();

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(Notification entity, NotificationDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.UserId = entity.UserId;
        dbo.Title = entity.Title;
        dbo.Message = entity.Message;
        dbo.IsRead = entity.IsRead;
        dbo.RelatedEntityId = entity.RelatedEntityId;
        dbo.NotificationType = entity.NotificationType;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(Notification? existingEntity, Notification newEntity,
        NotificationDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.UserId = newEntity.UserId;
        dbo.Title = newEntity.Title;
        dbo.Message = newEntity.Message;
        dbo.IsRead = newEntity.IsRead;
        dbo.RelatedEntityId = newEntity.RelatedEntityId;
        dbo.NotificationType = newEntity.NotificationType;

        return Task.CompletedTask;
    }

    protected override List<FilterDefinition<NotificationDbo>> BuildSpecificFilters(
        NotificationSearchRequest request)
    {
        var filters = new List<FilterDefinition<NotificationDbo>>();
        var builder = Builders<NotificationDbo>.Filter;

        if (request.UserId is not null) 
            filters.Add(builder.Eq(x => x.UserId, request.UserId));

        return filters;
    }
}