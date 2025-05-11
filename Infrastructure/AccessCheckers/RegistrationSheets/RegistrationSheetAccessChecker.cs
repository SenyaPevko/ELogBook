using Domain.Entities.RegistrationSheet;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.RegistrationSheets;

public class RegistrationSheetAccessChecker(IRequestContext context) : AccessCheckerBase<RegistrationSheet>(context)
{
}