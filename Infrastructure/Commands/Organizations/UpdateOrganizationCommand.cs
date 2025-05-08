using Domain.Dtos;
using Domain.Entities.Organization;
using Domain.Entities.Users;
using Domain.Repository;
using Domain.RequestArgs.Organizations;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.Organizations;

public class UpdateOrganizationCommand(IRepository<Organization, InvalidOrganizationReason> repository)
    : UpdateCommandBase<OrganizationDto, Organization, OrganizationUpdateArgs, InvalidOrganizationReason>(repository)
{
    protected override async Task<OrganizationDto> MapToDtoAsync(Organization entity) => await entity.ToDto();

    protected override Task ApplyUpdatesAsync(Organization entity, OrganizationUpdateArgs args)
    {
        if (args.Name is not null) entity.Name = args.Name;
        if (args.UserIds is not null)
        {
            if (args.UserIds.Add is not null)
                entity.UserIds.AddRange(args.UserIds.Add);
            if (args.UserIds.Remove is not null)
                entity.UserIds = entity.UserIds.Except(args.UserIds.Remove).ToList();
        }

        return Task.CompletedTask;
    }
}