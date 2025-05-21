using Domain.AccessChecker;
using Domain.Dtos.ConstructionSite;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.FileStorage;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class GetConstructionSiteCommand(
    IRepository<ConstructionSite> repository,
    IAccessChecker<ConstructionSite> accessChecker,
    IRepository<Organization> organizationRepository,
    IFileStorageService fileStorageService)
    : GetCommandBase<ConstructionSiteDto, ConstructionSite>(repository, accessChecker)
{
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(
        ConstructionSite entity)
    {
        return await entity.ToDto(fileStorageService, organizationRepository);
    }
}