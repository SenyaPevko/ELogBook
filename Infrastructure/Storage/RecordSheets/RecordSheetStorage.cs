using Domain.Entities.RecordSheet;
using Domain.Entities.RegistrationSheet;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.RecordSheets;
using Infrastructure.Helpers.SearchRequestHelper;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.RecordSheets;

public class RecordSheetStorage(
    AppDbContext context,
    IRequestContext requestContext,
    IStorage<RecordSheetItem> recItemStorage)
    : StorageBase<RecordSheet, RecordSheetDbo>(context, requestContext)
{
    private readonly AppDbContext _context = context;
    protected override IMongoCollection<RecordSheetDbo> Collection => _context.RecordSheets;

    protected override async Task MapEntityFromDboAsync(RecordSheet entity, RecordSheetDbo dbo)
    {
        var searchRequest =
            new SearchRequest().WhereIn<RegistrationSheetItem, Guid>(item => item.Id, dbo.RecordSheetItemIds);
        entity.Id = dbo.Id;
        entity.Number = dbo.Number;
        entity.Items = await recItemStorage.SearchAsync(searchRequest);
    }

    protected override Task MapDboFromEntityAsync(RecordSheet entity, RecordSheetDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.Number = entity.Number;
        dbo.RecordSheetItemIds = entity.Items.Select(item => item.Id).ToList();
        
        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(RecordSheet? existingEntity, RecordSheet newEntity,
        RecordSheetDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.Number = newEntity.Number;
        dbo.RecordSheetItemIds = newEntity.Items.Select(item => item.Id).ToList();
        
        return Task.CompletedTask;
    }
}