using Domain.Entities.RecordSheet;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.RecordSheets;

public class RecordSheetAccessChecker(IRequestContext context) : AccessCheckerBase<RecordSheet>(context)
{
    
}