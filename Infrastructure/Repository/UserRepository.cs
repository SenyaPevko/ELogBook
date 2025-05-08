using Domain;
using Domain.Entities.Organization;
using Domain.Entities.Users;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Helpers.SearchRequestHelper;
using Infrastructure.WriteContext;

namespace Infrastructure.Repository;

public class UserRepository(IStorage<User> storage, IStorage<Organization> organizationStorage)
    : RepositoryBase<User, InvalidUserReason>(storage)
{
    public override async Task AddAsync(User user, IWriteContext<InvalidUserReason> writeContext,
        CancellationToken cancellationToken)
    {
        await base.AddAsync(user, writeContext, cancellationToken);
    }

    protected override async Task ValidateCreationAsync(
        User entity, IWriteContext<InvalidUserReason> writeContext, CancellationToken cancellationToken)
    {
        var existingUsers = await SearchAsync(
            new SearchRequest().WhereEquals<User, string>(u => u.Email, entity.Email).SinglePage(),
            cancellationToken);
        if (existingUsers.Count != 0)
            writeContext.AddInvalidData(new ErrorDetail<InvalidUserReason>
            {
                Path = nameof(entity.Email),
                Reason = InvalidUserReason.EmailAlreadyExists,
                Value = entity.Email
            });
    }

    protected override async Task AfterCreateAsync(
        User newEntity,
        IWriteContext<InvalidUserReason> writeContext,
        CancellationToken cancellationToken)
    {
        await AddUserToOrganizationAsync(newEntity, writeContext);
    }
    
    protected override async Task AfterUpdateAsync(
        User? oldEntity,
        User newEntity,
        IWriteContext<InvalidUserReason> writeContext,
        CancellationToken cancellationToken)
    {
        await AddUserToOrganizationAsync(newEntity, writeContext);

        if (oldEntity is not null) 
            await RemoveUserFromOrganizationAsync(oldEntity);
    }

    private async Task AddUserToOrganizationAsync(User user, IWriteContext<InvalidUserReason> writeContext)
    {
        if (user.OrganizationId is not null)
        {
            var newOrganization = await organizationStorage.GetByIdAsync(user.OrganizationId.Value);
            if (newOrganization is null)
            {
                writeContext.AddInvalidData(new ErrorDetail<InvalidUserReason>
                {
                    Path = nameof(user.OrganizationId),
                    Reason = InvalidUserReason.ReferenceNotFound,
                    Value = user.OrganizationId.ToString()
                });
                return;
            }
            newOrganization.UserIds.Add(user.Id);

            await organizationStorage.UpdateAsync(newOrganization);
        }
    }
    
    private async Task RemoveUserFromOrganizationAsync(User user)
    {
        if (user.OrganizationId is not null)
        {
            var oldOrganization = await organizationStorage.GetByIdAsync(user.OrganizationId.Value);
            if (oldOrganization is null)
                return;
            
            oldOrganization.UserIds.Remove(user.Id);

            await organizationStorage.UpdateAsync(oldOrganization);
        }
    }
}