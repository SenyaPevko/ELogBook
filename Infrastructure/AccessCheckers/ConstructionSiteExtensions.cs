using Domain.Entities.ConstructionSite;
using Domain.Entities.Roles;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers;

public static class ConstructionSiteExtensions
{
    public static List<ConstructionSiteUserRoleType> GetUserRoleTypes(this ConstructionSite entity, IRequestContext context) =>
        entity.ConstructionSiteUserRoles
            .Where(r => r.UserId == context.Auth.UserId)
            .Select(r => r.Role)
            .ToList();
}