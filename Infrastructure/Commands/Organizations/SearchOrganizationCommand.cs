using Domain.AccessChecker;
using Domain.Dtos;
using Domain.Entities.Organization;
using Domain.Repository;
using Domain.RequestArgs.Organizations;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Organizations;

public class SearchOrganizationCommand(
    IRepository<Organization, InvalidOrganizationReason, OrganizationSearchRequest> repository,
    IAccessChecker<Organization> accessChecker)
    : SearchCommandBase<OrganizationDto, Organization, InvalidOrganizationReason, OrganizationSearchRequest>(repository,
        accessChecker)
{
    protected override async Task<OrganizationDto> MapToDtoAsync(Organization entity)
    {
        return await entity.ToDto();
    }
}