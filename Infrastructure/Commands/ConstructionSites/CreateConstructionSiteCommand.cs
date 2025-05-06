using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Repository;
using Domain.RequestArgs.CreationArgs;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class CreateConstructionSiteCommand(
    IRepository<ConstructionSite, InvalidConstructionSiteReason> repository)
    : CreateCommandBase<ConstructionSiteDto, ConstructionSite,
        ConstructionSiteCreationArgs, InvalidConstructionSiteReason>(repository)
{
    protected override Task<ConstructionSite> MapToEntityAsync(
        ConstructionSiteCreationArgs args)
    {
        return Task.FromResult(new ConstructionSite
        {
            Id = args.Id,
            Name = args.Name,
            Description = args.Description,
            Address = args.Address,
            Image = args.Image
        });
    }

    protected override async Task<ConstructionSiteDto> MapToDtoAsync(
        ConstructionSite entity)
    {
        return await entity.ToDto();
    }
}