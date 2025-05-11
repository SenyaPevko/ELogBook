using Domain.AccessChecker;
using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class UpdateConstructionSiteCommand(
    IRepository<ConstructionSite, InvalidConstructionSiteReason> repository,
    IAccessChecker<ConstructionSite, ConstructionSiteUpdateArgs> accessChecker)
    : UpdateCommandBase<ConstructionSiteDto, ConstructionSite,
        ConstructionSiteUpdateArgs, InvalidConstructionSiteReason>(repository, accessChecker)
{
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(ConstructionSite entity) => await entity.ToDto();
    
    protected override Task ApplyUpdatesAsync(ConstructionSite entity,
        ConstructionSiteUpdateArgs args)
    {
        if (args.Name is not null) entity.Name = args.Name;
        if (args.Description is not null) entity.Description = args.Description;
        if (args.Address is not null) entity.Address = args.Address;
        if (args.Image is not null) entity.Image = args.Image;
        if (args.Orders is not null)
        {
            if(args.Orders.Add is not null)
                entity.Orders.AddRange(args.Orders.Add.Select(MapArgsToEntity));
            if(args.Orders.Remove is not null)
                entity.Orders = entity.Orders.Where(o => !args.Orders.Remove.Contains(o.Id)).ToList();
        }
        if (args.UserRoles is not null)
        {
            if(args.UserRoles.Add is not null)
                entity.ConstructionSiteUserRoles.AddRange(args.UserRoles.Add.Select(MapArgsToEntity));
            // todo: какая-то хардорная логика, нужно подумать как оптимизировать
            // todo: нужно вообще подумать как IListUpdate автоматизировать
            if (args.UserRoles.Update is not null)
            {
                var idToRole = args.UserRoles.Update.ToDictionary(r => r.Id);
                foreach (var user in entity.ConstructionSiteUserRoles)
                {
                    if (idToRole.TryGetValue(user.Id, out var role))
                    {
                        ApplyUpdate(user, role);
                    }
                }
            }
            
            if(args.UserRoles.Remove is not null)
                entity.ConstructionSiteUserRoles = entity.ConstructionSiteUserRoles
                    .Where(r => !args.UserRoles.Remove.Contains(r.Id)).ToList();
        }

        return Task.FromResult(entity);
    }

    private ConstructionSiteUserRole MapArgsToEntity(ConstructionSiteUserRoleCreationArgs args) => new ConstructionSiteUserRole
    {
        Id = Guid.NewGuid(),
        Role = args.Role,
        UserId = args.UserId,
    };
    
    private Order MapArgsToEntity(OrderCreationArgs args) => new()
    {
        Id = Guid.NewGuid(),
        Link = args.Link,
        UserInChargeId = args.UserInChargeId,
    };

    private void ApplyUpdate(ConstructionSiteUserRole userRole, ConstructionSiteUserRoleUpdateArgs args)
    {
        userRole.Role = args.Role;
    }
}