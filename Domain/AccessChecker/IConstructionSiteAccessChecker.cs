using Domain.Entities.ConstructionSite;
using Domain.RequestArgs.ConstructionSites;

namespace Domain.AccessChecker;

public interface IConstructionSiteAccessChecker : IAccessChecker<ConstructionSite, ConstructionSiteUpdateArgs>
{
    Task<bool> CanUpdateOrders(ConstructionSite entity);
}