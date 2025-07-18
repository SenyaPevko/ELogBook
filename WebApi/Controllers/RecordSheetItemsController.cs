using Domain.Dtos.RecordSheet;
using Domain.Entities.RecordSheet;
using Domain.RequestArgs.RecordSheetItems;
using ELogBook.Controllers.Base;

namespace ELogBook.Controllers;

public class RecordSheetItemsController
    : CreatableEntityControllerBase<RecordSheetItemDto, RecordSheetItem, RecordSheetItemCreationArgs,
        RecordSheetItemUpdateArgs, InvalidRecordSheetItemReason, RecordSheetItemSearchRequest>
{
}