using Domain.Dtos;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class GetConstructionSiteCommand(IRepository<Domain.Entities.ConstructionSite.ConstructionSite> repository)
    : GetCommandBase<ConstructionSiteDto, Domain.Entities.ConstructionSite.ConstructionSite>(repository)
{
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(
        Domain.Entities.ConstructionSite.ConstructionSite entity) => await entity.ToDto();
}