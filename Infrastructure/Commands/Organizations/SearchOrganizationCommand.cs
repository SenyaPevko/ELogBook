using Domain.Dtos;
using Domain.Entities.Organization;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Organizations;

public class SearchOrganizationCommand(IRepository<Organization, InvalidOrganizationReason, OrganizationSearchRequest> repository)
    : SearchCommandBase<OrganizationDto, Organization, InvalidOrganizationReason, OrganizationSearchRequest>(repository)
{
    protected override async Task<OrganizationDto> MapToDtoAsync(Organization entity) => await entity.ToDto();
}