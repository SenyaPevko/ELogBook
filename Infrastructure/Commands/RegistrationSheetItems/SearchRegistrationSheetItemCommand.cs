using Domain.Dtos.RegistrationSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.RegistrationSheetItems;

public class SearchRegistrationSheetItemCommand(IRepository<RegistrationSheetItem> repository)
    : SearchCommandBase<RegistrationSheetItemDto, RegistrationSheetItem>(repository)
{
    protected override async Task<RegistrationSheetItemDto> MapToDtoAsync(RegistrationSheetItem entity) =>
        await entity.ToDto();
}