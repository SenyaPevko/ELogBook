using Domain.Entities.RegistrationSheet;
using Domain.RequestArgs.RegistrationSheetItems;

namespace Domain.AccessChecker;

public interface IRegistrationSheetItemAccessChecker 
    : IAccessChecker<RegistrationSheetItem, RegistrationSheetItemUpdateArgs>,
        IEntityUnderConstructionSiteAccessChecker<RegistrationSheetItemUpdateArgs>
{
    
}