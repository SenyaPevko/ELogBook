using Domain.Entities.RegistrationSheet;
using Domain.RequestArgs.RegistrationSheetItems;
using Domain.RequestArgs.RegistrationSheets;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.RegistrationSheets;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.RegistrationSheets;

public class RegistrationSheetStorage(
    AppDbContext context,
    IRequestContext requestContext,
    IStorage<RegistrationSheetItem, RegistrationSheetItemSearchRequest> regItemStorage)
    : StorageBase<RegistrationSheet, RegistrationSheetDbo, RegistrationSheetSearchRequest>(requestContext)
{
    protected override IMongoCollection<RegistrationSheetDbo> Collection => context.RegistrationSheets;

    protected override async Task MapEntityFromDboAsync(RegistrationSheet entity, RegistrationSheetDbo dbo)
    {
        var searchRequest = new RegistrationSheetItemSearchRequest { Ids = dbo.RegistrationSheetItemIds };
        entity.Id = dbo.Id;
        entity.Items = await regItemStorage.SearchAsync(searchRequest);
        entity.ConstructionSiteId = dbo.ConstructionSiteId;
    }

    protected override Task MapDboFromEntityAsync(RegistrationSheet entity, RegistrationSheetDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.RegistrationSheetItemIds = entity.Items.Select(item => item.Id).ToList();
        dbo.ConstructionSiteId = entity.ConstructionSiteId;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(RegistrationSheet? existingEntity, RegistrationSheet newEntity,
        RegistrationSheetDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.RegistrationSheetItemIds = newEntity.Items.Select(item => item.Id).ToList();
        dbo.ConstructionSiteId = newEntity.ConstructionSiteId;

        return Task.CompletedTask;
    }
}