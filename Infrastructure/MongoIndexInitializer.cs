using Infrastructure.Dbo.ConstructionSite;
using Infrastructure.Dbo.User;
using MongoDB.Driver;

namespace Infrastructure;

public class MongoIndexInitializer(IMongoDatabase database)
{
    public async Task EnsureIndexesAsync()
    {
        var userCollection = database.GetCollection<UserDbo>("users");

        await userCollection.Indexes.CreateOneAsync(
            new CreateIndexModel<UserDbo>(
                Builders<UserDbo>.IndexKeys.Ascending(u => u.Email),
                new CreateIndexOptions { Background = true, Unique = true }));

        await userCollection.Indexes.CreateOneAsync(
            new CreateIndexModel<UserDbo>(
                Builders<UserDbo>.IndexKeys.Ascending(u => u.RefreshToken),
                new CreateIndexOptions<UserDbo>
                {
                    Background = true,
                    Unique = true,
                    PartialFilterExpression = Builders<UserDbo>.Filter.Exists(u => u.RefreshToken)
                }));

        var siteCollection = database.GetCollection<ConstructionSiteDbo>("constructionSites");

        await siteCollection.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys.Ascending(s => s.Address),
                new CreateIndexOptions { Background = true, Unique = true }));

        await siteCollection.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys.Text(s => s.Name)
                    .Text(s => s.Description)
                    .Text(s => s.Address)));

        await siteCollection.Indexes.CreateOneAsync(
            new CreateIndexModel<ConstructionSiteDbo>(
                Builders<ConstructionSiteDbo>.IndexKeys.Ascending(s => s.Orders)));
    }
}