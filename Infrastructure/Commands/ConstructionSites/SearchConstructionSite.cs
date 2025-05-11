using Domain.AccessChecker;
using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class SearchConstructionSite(
    IRepository<ConstructionSite, InvalidConstructionSiteReason, ConstructionSiteSearchRequest> repository,
    IAccessChecker<ConstructionSite> accessChecker)
    : SearchCommandBase<ConstructionSiteDto, ConstructionSite, InvalidConstructionSiteReason,
        ConstructionSiteSearchRequest>(repository, accessChecker)
{
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(ConstructionSite entity)
    {
        return await entity.ToDto();
    }
}