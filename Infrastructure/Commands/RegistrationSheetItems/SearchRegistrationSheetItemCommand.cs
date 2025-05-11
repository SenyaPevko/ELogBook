using Domain.AccessChecker;
using Domain.Dtos.RegistrationSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.RegistrationSheetItems;

public class SearchRegistrationSheetItemCommand(
    IRepository<RegistrationSheetItem, InvalidRegistrationSheetItemReason, RegistrationSheetItemSearchRequest>
        repository,
    IAccessChecker<RegistrationSheetItem> accessChecker)
    : SearchCommandBase<RegistrationSheetItemDto, RegistrationSheetItem, InvalidRegistrationSheetItemReason,
        RegistrationSheetItemSearchRequest>(repository, accessChecker)
{
    protected override async Task<RegistrationSheetItemDto> MapToDtoAsync(RegistrationSheetItem entity) =>
        await entity.ToDto();
}