using Domain.AccessChecker;
using Domain.Dtos;
using Domain.Entities.Organization;
using Domain.Repository;
using Domain.RequestArgs.Organizations;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Organizations;

public class CreateOrganizationCommand(
    IRepository<Organization, InvalidOrganizationReason> repository,
    IAccessChecker<Organization> accessChecker)
    : CreateCommandBase<OrganizationDto, Organization, OrganizationCreationArgs, InvalidOrganizationReason>(repository,
        accessChecker)
{
    protected override async Task<OrganizationDto> MapToDtoAsync(Organization entity) => await entity.ToDto();

    protected override Task<Organization> MapToEntityAsync(OrganizationCreationArgs args) =>
        Task.FromResult(new Organization
        {
            Id = args.Id,
            Name = args.Name,
        });
}