using Domain.AccessChecker;
using Domain.Dtos.ConstructionSite;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.Entities.Roles;
using Domain.FileStorage;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class CreateConstructionSiteCommand(
    IRepository<ConstructionSite, InvalidConstructionSiteReason> repository,
    IAccessChecker<ConstructionSite> accessChecker,
    IRepository<Organization> organizationRepository,
    IFileStorageService fileStorageService)
    : CreateCommandBase<ConstructionSiteDto, ConstructionSite,
        ConstructionSiteCreationArgs, InvalidConstructionSiteReason>(repository, accessChecker)
{
    protected override Task<ConstructionSite> MapToEntityAsync(
        ConstructionSiteCreationArgs args)
    {
        return Task.FromResult(new ConstructionSite
        {
            Id = args.Id,
            ShortName = args.ShortName,
            FullName = args.FullName,
            Address = args.Address,
            OrganizationId = args.OrganizationId,
            SubOrganizationId = args.SubOrganizationId,
            ConstructionSiteUserRoles = args.UserIds.Select(i => new ConstructionSiteUserRole
            {
                Id = Guid.NewGuid(),
                UserId = i,
                Role = ConstructionSiteUserRoleType.AuthorSupervision
            }).ToList()
        });
    }

    protected override async Task<ConstructionSiteDto> MapToDtoAsync(
        ConstructionSite entity)
    {
        return await entity.ToDto(fileStorageService, organizationRepository);
    }
}