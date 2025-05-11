using Infrastructure.Context;
using Infrastructure.Dbo;
using Infrastructure.Dbo.ConstructionSite;
using Infrastructure.Dbo.RecordSheets;
using Infrastructure.Dbo.User;
using MongoDB.Driver;

namespace Infrastructure;

public class MongoIndexInitializer(AppDbContext appDbContext)
{
    public async Task EnsureIndexesAsync()
    {
        await CreateUserIndexesAsync(appDbContext);
        await CreateConstructionSiteIndexesAsync(appDbContext);
        await CreateOrganizationIndexesAsync(appDbContext);
        await CreateRecordSheetItemIndexesAsync(appDbContext);
        // todo: добавить индексы для всех сущностей
    }

    private static async Task CreateUserIndexesAsync(AppDbContext appDbContext)
    {
        await appDbContext.Users.Indexes.CreateOneAsync(
            new CreateIndexModel<UserDbo>(
                Builders<UserDbo>.IndexKeys.Ascending(u => u.Email),
                new CreateIndexOptions { Background = true, Unique = true }));

        await appDbContext.Users.Indexes.CreateOneAsync(
            new CreateIndexModel<UserDbo>(
                Builders<UserDbo>.IndexKeys.Ascending(u => u.RefreshToken),
                new CreateIndexOptions<UserDbo>
                {
                    Background = true,
                    Unique = true,
                    PartialFilterExpression = Builders<UserDbo>.Filter.Exists(u => u.RefreshToken)
                }));
    }

    private static async Task CreateConstructionSiteIndexesAsync(AppDbContext appDbContext)
    {
        await appDbContext.ConstructionSites.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys.Ascending(s => s.Address),
                new CreateIndexOptions { Background = true, Unique = true }));
        
        await appDbContext.ConstructionSites.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys.Ascending(s => s.RegistrationSheetId),
                new CreateIndexOptions { Background = true, Unique = true }));
        
        await appDbContext.ConstructionSites.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys.Ascending(s => s.RecordSheetId),
                new CreateIndexOptions { Background = true, Unique = true }));
        
        await appDbContext.ConstructionSites.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys.Ascending(s => s.WorkIssueId),
                new CreateIndexOptions { Background = true, Unique = true }));

        await appDbContext.ConstructionSites.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys.Text(s => s.Name)
                    .Text(s => s.Description)
                    .Text(s => s.Address)));

        await appDbContext.ConstructionSites.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys.Ascending(s => s.Orders)));

        await appDbContext.ConstructionSites.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys.Ascending(x => x.ConstructionSiteUserRoles.First().UserId),
                new CreateIndexOptions { Background = true }));

        await appDbContext.ConstructionSites.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys
                    .Ascending(x => x.ConstructionSiteUserRoles.First().UserId)
                    .Ascending(x => x.ConstructionSiteUserRoles.First().Role),
                new CreateIndexOptions { Background = true }));
    }

    private static async Task CreateOrganizationIndexesAsync(AppDbContext appDbContext)
    {
        await appDbContext.Organizations.Indexes.CreateOneAsync(
            new CreateIndexModel<OrganizationDbo>(
                Builders<OrganizationDbo>.IndexKeys.Ascending(u => u.Name),
                new CreateIndexOptions { Background = true, Unique = true }));
    }
    
    private static async Task CreateRecordSheetItemIndexesAsync(AppDbContext appDbContext)
    {
        await appDbContext.RecordSheetItems.Indexes.CreateOneAsync(
            new CreateIndexModel<RecordSheetItemDbo>(
                Builders<RecordSheetItemDbo>.IndexKeys.Ascending(s => s.RecordSheetId),
                new CreateIndexOptions { Background = true}));
    }
}