using Core.Helpers;
using Domain.AccessChecker;
using Domain.Dtos.ConstructionSite;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.FileStorage;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Infrastructure.Commands.Base;
using MongoDB.Bson;

namespace Infrastructure.Commands.ConstructionSites;

public class UpdateConstructionSiteCommand(
    IRepository<ConstructionSite, InvalidConstructionSiteReason> repository,
    IAccessChecker<ConstructionSite, ConstructionSiteUpdateArgs> accessChecker,
    IFileStorageService fileStorage,
    IRepository<Organization> organizationRepository)
    : UpdateCommandBase<ConstructionSiteDto, ConstructionSite,
        ConstructionSiteUpdateArgs, InvalidConstructionSiteReason>(repository, accessChecker)
{
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(ConstructionSite entity)
    {
        return await entity.ToDto(fileStorage, organizationRepository);
    }

    protected override async Task ApplyUpdatesAsync(ConstructionSite entity,
        ConstructionSiteUpdateArgs args)
    {
        if (args.Name is not null) entity.ShortName = args.Name;
        if (args.OrganizationId is not null) entity.OrganizationId = args.OrganizationId.Value;
        if (args.SubOrganizationId is not null) entity.SubOrganizationId = args.SubOrganizationId.Value;
        if (args.Description is not null) entity.FullName = args.Description;
        if (args.Address is not null) entity.Address = args.Address;
        if (args.Orders is not null)
        {
            if (args.Orders.Add is not null)
                entity.Orders.AddRange(await args.Orders.Add.SelectAsync(MapArgsToEntity));
            if (args.Orders.Remove is not null)
                // todo: полагаю файл тоже нужно удалять, который order принадлежит
                entity.Orders = entity.Orders.Where(o => !args.Orders.Remove.Contains(o.Id)).ToList();
        }

        if (args.UserRoles is not null)
        {
            if (args.UserRoles.Add is not null)
                entity.ConstructionSiteUserRoles.AddRange(args.UserRoles.Add.Select(MapArgsToEntity));
            // todo: какая-то хардорная логика, нужно подумать как оптимизировать
            // todo: нужно вообще подумать как IListUpdate автоматизировать
            if (args.UserRoles.Update is not null)
            {
                var idToRole = args.UserRoles.Update.ToDictionary(r => r.Id);
                foreach (var user in entity.ConstructionSiteUserRoles)
                    if (idToRole.TryGetValue(user.Id, out var role))
                        ApplyUpdate(user, role);
            }

            if (args.UserRoles.Remove is not null)
                entity.ConstructionSiteUserRoles = entity.ConstructionSiteUserRoles
                    .Where(r => !args.UserRoles.Remove.Contains(r.Id)).ToList();
        }
    }

    private ConstructionSiteUserRole MapArgsToEntity(ConstructionSiteUserRoleCreationArgs args)
    {
        return new ConstructionSiteUserRole
        {
            Id = Guid.NewGuid(),
            Role = args.Role,
            UserId = args.UserId
        };
    }

    private async Task<Order> MapArgsToEntity(OrderCreationArgs args)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            FileId = new ObjectId(args.FileId),
            UserInChargeId = args.UserInChargeId
        };
    }

    private void ApplyUpdate(ConstructionSiteUserRole userRole, ConstructionSiteUserRoleUpdateArgs args)
    {
        userRole.Role = args.Role;
    }
}