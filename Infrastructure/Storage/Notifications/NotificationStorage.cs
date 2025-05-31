using Domain.Entities.Notifications;
using Domain.RequestArgs.Notifications;
using Infrastructure.Context;
using Infrastructure.Dbo.Notifications;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.Notifications;

public class NotificationStorage(AppDbContext dbContext, IRequestContext requestContext)
    : StorageBase<RecordSheetItemNotification, RecordSheetItemNotificationDbo, NotificationSearchRequest>(
        requestContext)
{
    protected override IMongoCollection<RecordSheetItemNotificationDbo> Collection => dbContext.Notifications;

    protected override Task MapEntityFromDboAsync(RecordSheetItemNotification entity,
        RecordSheetItemNotificationDbo dbo)
    {
        entity.Id = dbo.Id;
        entity.UserId = dbo.UserId;
        entity.Title = dbo.Title;
        entity.Message = dbo.Message;
        entity.IsRead = dbo.IsRead;
        entity.RecordSheetItemId = dbo.RecordSheetItemId;
        entity.RecordSheetId = dbo.RecordSheetId;
        entity.ConstructionSiteId = dbo.ConstructionSiteId;
        entity.UpdateInfo = dbo.ToUpdateInfo();

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(RecordSheetItemNotification entity,
        RecordSheetItemNotificationDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.UserId = entity.UserId;
        dbo.Title = entity.Title;
        dbo.Message = entity.Message;
        dbo.IsRead = entity.IsRead;
        dbo.RecordSheetItemId = entity.RecordSheetItemId;
        dbo.RecordSheetId = entity.RecordSheetId;
        dbo.ConstructionSiteId = entity.ConstructionSiteId;
        dbo.NotificationType = entity.NotificationType;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(RecordSheetItemNotification? existingEntity,
        RecordSheetItemNotification newEntity,
        RecordSheetItemNotificationDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.UserId = newEntity.UserId;
        dbo.Title = newEntity.Title;
        dbo.Message = newEntity.Message;
        dbo.IsRead = newEntity.IsRead;
        dbo.RecordSheetItemId = newEntity.RecordSheetItemId;
        dbo.RecordSheetId = newEntity.RecordSheetId;
        dbo.ConstructionSiteId = newEntity.ConstructionSiteId;
        dbo.NotificationType = newEntity.NotificationType;

        return Task.CompletedTask;
    }

    protected override List<FilterDefinition<RecordSheetItemNotificationDbo>> BuildSpecificFilters(
        NotificationSearchRequest request)
    {
        var filters = new List<FilterDefinition<RecordSheetItemNotificationDbo>>();
        var builder = Builders<RecordSheetItemNotificationDbo>.Filter;

        if (request.UserId is not null)
            filters.Add(builder.Eq(x => x.UserId, request.UserId));

        return filters;
    }
    
    protected override bool IsEmptySearchRequest(NotificationSearchRequest request)
    {
        return request.UserId is null;
    }
}