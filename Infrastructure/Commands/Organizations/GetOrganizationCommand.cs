using Domain.Dtos;
using Domain.Entities.Organization;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Organizations;

public class GetOrganizationCommand(IRepository<Organization> repository) 
    : GetCommandBase<OrganizationDto, Organization>(repository)
{
    protected override async Task<OrganizationDto> MapToDtoAsync(Organization entity) => await entity.ToDto();
}