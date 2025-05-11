using Domain.AccessChecker;
using Domain.Dtos.RegistrationSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.RegistrationSheetItems;

public class GetRegistrationSheetItemCommand(
    IRepository<RegistrationSheetItem> repository,
    IAccessChecker<RegistrationSheetItem> accessChecker)
    : GetCommandBase<RegistrationSheetItemDto, RegistrationSheetItem>(repository, accessChecker)
{
    protected override async Task<RegistrationSheetItemDto> MapToDtoAsync(RegistrationSheetItem entity) =>
        await entity.ToDto();
}