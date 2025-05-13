using Domain.AccessChecker;
using Domain.Dtos;
using Domain.Dtos.ConstructionSite;
using Domain.Entities.ConstructionSite;
using Domain.FileStorage;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class CreateConstructionSiteCommand(
    IRepository<ConstructionSite, InvalidConstructionSiteReason> repository,
    IAccessChecker<ConstructionSite> accessChecker,
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
            Name = args.Name,
            Description = args.Description,
            Address = args.Address,
            OrganizationId = args.OrganizationId,
        });
    }

    protected override async Task<ConstructionSiteDto> MapToDtoAsync(
        ConstructionSite entity)
    {
        return await entity.ToDto(fileStorageService);
    }
}