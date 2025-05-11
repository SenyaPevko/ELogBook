using Domain.Settings;
using Infrastructure.Dbo;
using Infrastructure.Dbo.ConstructionSite;
using Infrastructure.Dbo.RecordSheets;
using Infrastructure.Dbo.RegistrationSheets;
using Infrastructure.Dbo.User;
using Infrastructure.Dbo.WorkIssues;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Context;

public class AppDbContext
{
    private readonly IMongoDatabase _database;

    public AppDbContext(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
    {
        _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<UserDbo> Users =>
        _database.GetCollection<UserDbo>("users");

    public IMongoCollection<ConstructionSiteDbo> ConstructionSites =>
        _database.GetCollection<ConstructionSiteDbo>("constructionSites");

    public IMongoCollection<ConstructionSiteUserRoleDbo> ConstructionSiteUserRoles =>
        _database.GetCollection<ConstructionSiteUserRoleDbo>("constructionSiteUserRoles");

    public IMongoCollection<RecordSheetDbo> RecordSheets =>
        _database.GetCollection<RecordSheetDbo>("recordSheets");

    public IMongoCollection<RecordSheetItemDbo> RecordSheetItems =>
        _database.GetCollection<RecordSheetItemDbo>("recordSheetItems");

    public IMongoCollection<RegistrationSheetDbo> RegistrationSheets =>
        _database.GetCollection<RegistrationSheetDbo>("registrationSheets");

    public IMongoCollection<RegistrationSheetItemDbo> RegistrationSheetItems =>
        _database.GetCollection<RegistrationSheetItemDbo>("registrationSheetItems");

    public IMongoCollection<OrganizationDbo> Organizations =>
        _database.GetCollection<OrganizationDbo>("organizations");

    public IMongoCollection<WorkIssueItemDbo> WorkIssueItems =>
        _database.GetCollection<WorkIssueItemDbo>("workIssueItems");

    public IMongoCollection<WorkIssueDbo> WorkIssues =>
        _database.GetCollection<WorkIssueDbo>("workIssues");
}