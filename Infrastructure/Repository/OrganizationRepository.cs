using Domain;
using Domain.Entities.Organization;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Helpers.SearchRequestHelper;

namespace Infrastructure.Repository;

public class OrganizationRepository(IStorage<Organization> storage) 
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
}