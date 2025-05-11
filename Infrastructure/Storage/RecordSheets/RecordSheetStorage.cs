using Domain.Entities.RecordSheet;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.RecordSheets;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.RecordSheets;

public class RecordSheetStorage(
    AppDbContext context,
    IRequestContext requestContext,
    IStorage<RecordSheetItem, RecordSheetItemSearchRequest> recItemStorage)
    : StorageBase<RecordSheet, RecordSheetDbo, RecordSheetSearchRequest>(requestContext)
{
    protected override IMongoCollection<RecordSheetDbo> Collection => context.RecordSheets;

    protected override async Task MapEntityFromDboAsync(RecordSheet entity, RecordSheetDbo dbo)
    {
        var searchRequest = new RecordSheetItemSearchRequest { Ids = dbo.RecordSheetItemIds };
        entity.Id = dbo.Id;
        entity.Number = dbo.Number;
        entity.Items = await recItemStorage.SearchAsync(searchRequest);
        entity.ConstructionSiteId = dbo.ConstructionSiteId;
    }

    protected override Task MapDboFromEntityAsync(RecordSheet entity, RecordSheetDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.Number = entity.Number;
        dbo.RecordSheetItemIds = entity.Items.Select(item => item.Id).ToList();
        dbo.ConstructionSiteId = entity.ConstructionSiteId;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(RecordSheet? existingEntity, RecordSheet newEntity,
        RecordSheetDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.Number = newEntity.Number;
        dbo.RecordSheetItemIds = newEntity.Items.Select(item => item.Id).ToList();
        dbo.ConstructionSiteId = newEntity.ConstructionSiteId;

        return Task.CompletedTask;
    }
}