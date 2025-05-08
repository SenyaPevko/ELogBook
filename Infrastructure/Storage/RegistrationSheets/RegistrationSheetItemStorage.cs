using Domain.Entities;
using Domain.Entities.Organization;
using Domain.Entities.RegistrationSheet;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Context;
using Infrastructure.Dbo.RegistrationSheets;
using Infrastructure.Helpers.SearchRequestHelper;
using Infrastructure.Storage.Base;
using Infrastructure.Storage.Organizations;
using Infrastructure.Storage.Users;
using MongoDB.Driver;

namespace Infrastructure.Storage.RegistrationSheets;

public class RegistrationSheetItemStorage(
    AppDbContext context,
    IRequestContext requestContext,
    UserStorage userStorage,
    OrganizationStorage organizationStorage)
    : StorageBase<RegistrationSheetItem, RegistrationSheetItemDbo>(context, requestContext)
{
    private readonly AppDbContext _context = context;
    protected override IMongoCollection<RegistrationSheetItemDbo> Collection => _context.RegistrationSheetItems;

    protected override async Task MapEntityFromDboAsync(RegistrationSheetItem entity, RegistrationSheetItemDbo dbo)
    {
        var user = await userStorage.GetByIdAsync(dbo.CreatorId);
        var organization = 
        entity.Id = dbo.Id;
        entity.Name = user!.Name;
        entity.Surname = user.Surname;
        entity.Patronymic = user.Patronymic;
        entity.ArrivalDate = dbo.ArrivalDate;
        entity.DepartureDate = dbo.DepartureDate;
        entity.Signature = user.GetSignature();
    }

    protected override Task MapDboFromEntityAsync(RegistrationSheetItem entity, RegistrationSheetItemDbo dbo)
    {
        dbo.Id = entity.Id;
        var searchRequest = new SearchRequest().WhereEquals<Organization, string>(e => e.Name, entity.OrganizationName);
        dbo.OrganizationId = organizationStorage.SearchAsync();
        dbo.OrganizationId = entity.;
    }

    protected override Task MapDboFromEntityAsync(RegistrationSheetItem? existingEntity,
        RegistrationSheetItem newEntity,
        RegistrationSheetItemDbo dbo)
    {
        throw new NotImplementedException();
    }
}

}