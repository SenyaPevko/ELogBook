using Domain;
using Domain.Entities.Organization;
using Domain.Entities.Users;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Helpers.SearchRequestHelper;

namespace Infrastructure.Repository;

public class OrganizationRepository(IStorage<Organization> storage, IStorage<User> userStorage)
    : RepositoryBase<Organization, InvalidOrganizationReason>(storage)
{
    protected override async Task ValidateCreationAsync(
        Organization entity,
        IWriteContext<InvalidOrganizationReason> writeContext,
        CancellationToken cancellationToken)
    {
        var existingOrganizations = await SearchAsync(
            new SearchRequest().WhereEquals<Organization, string>(o => o.Name, entity.Name).SinglePage(),
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

        var searchRequest = new SearchRequest().WhereIn<User, Guid>(o => o.Id, addedUserIds);
        var addedUsers = await userStorage.SearchAsync(searchRequest);
        var addedUsersToId = addedUsers.ToDictionary(x => x.Id);

        foreach (var id in addedUserIds.Where(id => !addedUsersToId.ContainsKey(id)))
        {
            writeContext.AddInvalidData(new ErrorDetail<InvalidOrganizationReason>
            {
                Path = nameof(Organization.UserIds),
                Reason = InvalidOrganizationReason.ReferenceNotFound,
                Value = id.ToString()
            });
        }
    }
}