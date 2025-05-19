using Domain.Entities.Organization;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.Users;
using Domain.RequestArgs.Organizations;
using Domain.RequestArgs.RegistrationSheetItems;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.RegistrationSheets;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.RegistrationSheets;

public class RegistrationSheetItemStorage(
    AppDbContext context,
    IRequestContext requestContext,
    IStorage<User> userStorage,
    IStorage<Organization, OrganizationSearchRequest> organizationStorage)
    : StorageBase<RegistrationSheetItem, RegistrationSheetItemDbo, RegistrationSheetItemSearchRequest>(requestContext)
{
    protected override IMongoCollection<RegistrationSheetItemDbo> Collection => context.RegistrationSheetItems;

    protected override async Task MapEntityFromDboAsync(RegistrationSheetItem entity, RegistrationSheetItemDbo dbo)
    {
        var user = await userStorage.GetByIdAsync(dbo.CreatorId);
        var organization = await organizationStorage.GetByIdAsync(dbo.OrganizationId);
        entity.Id = dbo.Id;
        entity.Name = user!.Name;
        entity.Surname = user.Surname;
        entity.Patronymic = user.Patronymic;
        entity.ArrivalDate = dbo.ArrivalDate;
        entity.DepartureDate = dbo.DepartureDate;
        entity.Signature = user.GetSignature();
        entity.OrganizationName = organization?.Name ?? user.OrganizationName;
        entity.CreatorId = dbo.CreatorId;
        entity.RegistrationSheetId = dbo.RegistrationSheetId;
    }

    protected override async Task MapDboFromEntityAsync(RegistrationSheetItem entity, RegistrationSheetItemDbo dbo)
    {
        var searchRequest = new OrganizationSearchRequest { Name = entity.OrganizationName };
        var organization = (await organizationStorage.SearchAsync(searchRequest)).FirstOrDefault();
        dbo.Id = entity.Id;
        dbo.CreatorId = entity.CreatorId;
        dbo.ArrivalDate = entity.ArrivalDate;
        dbo.DepartureDate = entity.DepartureDate;
        dbo.RegistrationSheetId = entity.RegistrationSheetId;
        if (organization is not null) dbo.OrganizationId = organization.Id;
    }

    protected override async Task MapDboFromEntityAsync(RegistrationSheetItem? existingEntity,
        RegistrationSheetItem newEntity,
        RegistrationSheetItemDbo dbo)
    {
        var searchRequest = new OrganizationSearchRequest { Name = newEntity.OrganizationName };
        var organization = (await organizationStorage.SearchAsync(searchRequest)).FirstOrDefault();
        dbo.Id = newEntity.Id;
        dbo.CreatorId = newEntity.CreatorId;
        dbo.ArrivalDate = newEntity.ArrivalDate;
        dbo.DepartureDate = newEntity.DepartureDate;
        dbo.RegistrationSheetId = newEntity.RegistrationSheetId;
        if (organization is not null) dbo.OrganizationId = organization.Id;
    }
}