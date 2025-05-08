using Domain.Entities.RegistrationSheet;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.RegistrationSheets;
using Infrastructure.Helpers.SearchRequestHelper;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.RegistrationSheets;

public class RegistrationSheetStorage(AppDbContext context, IRequestContext requestContext, IStorage<RegistrationSheetItem> regItemStorage)
    : StorageBase<RegistrationSheet, RegistrationSheetDbo>(context, requestContext)
{
    private readonly AppDbContext _context = context;
    protected override IMongoCollection<RegistrationSheetDbo> Collection => _context.RegistrationSheets;

    protected override async Task MapEntityFromDboAsync(RegistrationSheet entity, RegistrationSheetDbo dbo)
    {
        entity.Id = dbo.Id;
        var searchRequest = new SearchRequest().WhereIn<RegistrationSheetItem, Guid>(item => item.Id, dbo.RegistrationSheetItemIds);
        entity.Items = await regItemStorage.SearchAsync(searchRequest);
    }

    protected override Task MapDboFromEntityAsync(RegistrationSheet entity, RegistrationSheetDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.RegistrationSheetItemIds = entity.Items.Select(item => item.Id).ToList();
        
        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(RegistrationSheet? existingEntity, RegistrationSheet newEntity,
        RegistrationSheetDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.RegistrationSheetItemIds = newEntity.Items.Select(item => item.Id).ToList();
        
        return Task.CompletedTask;
    }
}