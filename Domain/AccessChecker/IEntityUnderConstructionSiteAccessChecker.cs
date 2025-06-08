using Domain.Entities.Roles;
using Domain.RequestArgs.Base;

namespace Domain.AccessChecker;

public interface IEntityUnderConstructionSiteAccessChecker<in TUpdateArgs> where TUpdateArgs : IEntityUpdateArgs
{
    bool CanCreate(List<ConstructionSiteUserRoleType> userRoles);
    bool CanRead(List<ConstructionSiteUserRoleType> userRoles);
    bool CanUpdate(List<ConstructionSiteUserRoleType> userRoles);
    bool CanUpdate(TUpdateArgs updateArgs, List<ConstructionSiteUserRoleType> userRoles);
    Task<List<ConstructionSiteUserRoleType>> GetUserRoleTypes(Guid constructionSiteId);
}