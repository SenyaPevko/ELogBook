using Domain.AccessChecker;
using Domain.Dtos;
using Domain.Dtos.ConstructionSite;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.FileStorage;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class SearchConstructionSite(
    IRepository<ConstructionSite, InvalidConstructionSiteReason, ConstructionSiteSearchRequest> repository,
    IAccessChecker<ConstructionSite> accessChecker,
    IRepository<Organization> organizationRepository,
    IFileStorageService fileStorageService)
    : SearchCommandBase<ConstructionSiteDto, ConstructionSite, InvalidConstructionSiteReason,
        ConstructionSiteSearchRequest>(repository, accessChecker)
{
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(ConstructionSite entity)
    {
        return await entity.ToDto(fileStorageService, organizationRepository);
    }
}