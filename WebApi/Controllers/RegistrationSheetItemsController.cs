using Domain.Dtos.RegistrationSheet;
using Domain.Entities.RegistrationSheet;
using Domain.RequestArgs.RegistrationSheetItems;
using Domain.RequestArgs.SearchRequest;
using ELogBook.Controllers.Base;

namespace ELogBook.Controllers;

public class RegistrationSheetItemsController
    : CreatableEntityControllerBase<RegistrationSheetItemDto, RegistrationSheetItem, RegistrationSheetItemCreationArgs,
        RegistrationSheetItemUpdateArgs, InvalidRegistrationSheetItemReason, RegistrationSheetItemSearchRequest>
{
}