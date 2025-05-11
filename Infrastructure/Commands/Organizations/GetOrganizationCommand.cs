using Domain.AccessChecker;
using Domain.Dtos;
using Domain.Entities.Organization;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Organizations;

public class GetOrganizationCommand(IRepository<Organization> repository, IAccessChecker<Organization> accessChecker)
    : GetCommandBase<OrganizationDto, Organization>(repository, accessChecker)
{
    protected override async Task<OrganizationDto> MapToDtoAsync(Organization entity)
    {
        return await entity.ToDto();
    }
}