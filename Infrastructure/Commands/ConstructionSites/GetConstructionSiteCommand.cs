using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class GetConstructionSiteCommand(IRepository<ConstructionSite> repository)
    : GetCommandBase<ConstructionSiteDto, ConstructionSite>(repository)
{
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(
        ConstructionSite entity)
    {
        return await entity.ToDto();
    }
}