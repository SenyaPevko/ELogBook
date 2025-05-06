using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class SearchConstructionSite(IRepository<ConstructionSite> repository)
    : SearchCommandBase<ConstructionSiteDto, ConstructionSite>(repository)
{
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(ConstructionSite entity) => await entity.ToDto();
}