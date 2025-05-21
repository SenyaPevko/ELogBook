using Domain.Entities.Organization;
using Domain.Entities.Users;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.Organizations;
using Domain.RequestArgs.Users;
using Domain.Storage;

namespace Infrastructure.Repository;

public class OrganizationRepository(
    IStorage<Organization, OrganizationSearchRequest> storage,
    IStorage<User, UserSearchRequest> userStorage)
    : RepositoryBase<Organization, InvalidOrganizationReason, OrganizationSearchRequest>(storage)
{
    protected override async Task ValidateCreationAsync(
        Organization entity,
        IWriteContext<InvalidOrganizationReason> writeContext,
        CancellationToken cancellationToken)
    {
        var existingOrganizations = await SearchAsync(new OrganizationSearchRequest { Name = entity.Name },
            cancellationToken);
        if (existingOrganizations.Count != 0)
            writeContext.AddInvalidData(new ErrorDetail<InvalidOrganizationReason>
            {
                Path = nameof(entity.Name),
                Reason = InvalidOrganizationReason.NameAlreadyExists,
                Value = entity.Name
            });
    }

    protected override async Task ValidateUpdateAsync(Organization oldEntity, Organization newEntity,
        IWriteContext<InvalidOrganizationReason> writeContext, CancellationToken cancellationToken)
    {
        await ValidateAddedUsers(oldEntity, newEntity, writeContext);
    }

    private async Task ValidateAddedUsers(
        Organization oldEntity,
        Organization newEntity,
        IWriteContext<InvalidOrganizationReason> writeContext)
    {
        var addedUserIds = newEntity.UserIds.Except(oldEntity.UserIds).ToList();
        if (addedUserIds.Count == 0)
            return;

        var searchRequest = new UserSearchRequest { Ids = addedUserIds };
        var addedUsers = await userStorage.SearchAsync(searchRequest);
        var addedUsersToId = addedUsers.ToDictionary(x => x.Id);

        foreach (var id in addedUserIds.Where(id => !addedUsersToId.ContainsKey(id)))
            writeContext.AddInvalidData(new ErrorDetail<InvalidOrganizationReason>
            {
                Path = nameof(Organization.UserIds),
                Reason = InvalidOrganizationReason.ReferenceNotFound,
                Value = id.ToString()
            });
    }
}