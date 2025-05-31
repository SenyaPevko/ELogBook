using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.Entities.RecordSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.Users;
using Domain.Entities.WorkIssues;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Domain.RequestArgs.Users;
using Domain.Storage;
using ConstructionSite = Domain.Entities.ConstructionSite.ConstructionSite;

namespace Infrastructure.Repository;

public class ConstructionSiteRepository(
    IStorage<ConstructionSite, ConstructionSiteSearchRequest> storage,
    IStorage<RegistrationSheet> regStorage,
    IStorage<RecordSheet> recStorage,
    IStorage<WorkIssue> issueStorage,
    IStorage<User, UserSearchRequest> userStorage)
    : RepositoryBase<ConstructionSite, InvalidConstructionSiteReason, ConstructionSiteSearchRequest>(storage)
{
    protected override async Task ValidateCreationAsync(
        ConstructionSite entity,
        IWriteContext<InvalidConstructionSiteReason> writeContext,
        CancellationToken cancellationToken)
    {
        //todo: мб нужно изображения валидировать ._.

        // todo: мб здесь написать проверку на наличие RegistrationSheet RecordSheet WorkIssue в бд, мб они не записались типо
        // хотя их запись можно вообще в асинхрон увести, и кешировать, чтоб из базы не доставать

        var existingAddresses = await SearchAsync(new ConstructionSiteSearchRequest { Address = entity.Address },
            cancellationToken);
        if (existingAddresses.Count != 0)
            writeContext.AddInvalidData(new ErrorDetail<InvalidConstructionSiteReason>
            {
                Path = nameof(entity.Address),
                Reason = InvalidConstructionSiteReason.AddressAlreadyExists,
                Value = entity.Address
            });
    }

    protected override async Task ValidateUpdateAsync(
        ConstructionSite oldEntity,
        ConstructionSite newEntity,
        IWriteContext<InvalidConstructionSiteReason> writeContext,
        CancellationToken cancellationToken)
    {
        await ValidateAddedUsers(oldEntity, newEntity, writeContext);
    }

    protected override async Task PreprocessCreationAsync(
        ConstructionSite entity,
        IWriteContext<InvalidConstructionSiteReason> writeContext,
        CancellationToken cancellationToken)
    {
        var regSheet = new RegistrationSheet { Id = Guid.NewGuid(), ConstructionSiteId = entity.Id };
        // todo: непонятно что делать с полем Number - заказчик сказал, что уточнит и скажет
        var recSheet = new RecordSheet { Id = Guid.NewGuid(), ConstructionSiteId = entity.Id };
        var workIssue = new WorkIssue { Id = Guid.NewGuid(), ConstructionSiteId = entity.Id };

        await regStorage.AddAsync(regSheet);
        await recStorage.AddAsync(recSheet);
        await issueStorage.AddAsync(workIssue);

        entity.RegistrationSheet = regSheet;
        entity.RecordSheet = recSheet;
        entity.WorkIssue = workIssue;
    }

    protected async Task ValidateAddedUsers(
        ConstructionSite oldEntity,
        ConstructionSite newEntity,
        IWriteContext<InvalidConstructionSiteReason> writeContext)
    {
        var oldUserIds = oldEntity.ConstructionSiteUserRoles.Select(x => x.UserId).ToHashSet();
        var addedUserIds = newEntity.ConstructionSiteUserRoles
            .Skip(oldUserIds.Count)
            .Select(r => r.UserId)
            .ToList();

        var notUniqueUserIds = addedUserIds.Intersect(oldUserIds).ToList();
        foreach (var userId in notUniqueUserIds)
            writeContext.AddInvalidData(new ErrorDetail<InvalidConstructionSiteReason>
            {
                Path = nameof(Organization.UserIds),
                Reason = InvalidConstructionSiteReason.UserAlreadyHasRole,
                Value = userId.ToString()
            });

        if (addedUserIds.Count == 0)
            return;

        var searchRequest = new UserSearchRequest { Ids = addedUserIds };
        var addedUsers = await userStorage.SearchAsync(searchRequest);
        var addedUsersToId = addedUsers.ToDictionary(x => x.Id);

        foreach (var id in addedUserIds.Where(id => !addedUsersToId.ContainsKey(id)))
            writeContext.AddInvalidData(new ErrorDetail<InvalidConstructionSiteReason>
            {
                Path = nameof(Organization.UserIds),
                Reason = InvalidConstructionSiteReason.ReferenceNotFound,
                Value = id.ToString()
            });
    }
}