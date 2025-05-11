using Domain.AccessChecker;
using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class GetConstructionSiteCommand(IRepository<ConstructionSite> repository, IAccessChecker<ConstructionSite> accessChecker)
    : GetCommandBase<ConstructionSiteDto, ConstructionSite>(repository, accessChecker)
{
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(
        ConstructionSite entity)
    {
        return await entity.ToDto();
    }
}