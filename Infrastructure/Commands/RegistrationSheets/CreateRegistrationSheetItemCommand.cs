using Domain.Dtos.RegistrationSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Repository;
using Domain.RequestArgs.RegistrationSheetItems;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.RegistrationSheets;

// todo: при создании передается RegistrationSheetId - нужно сразу вставлять в список у RegistrationSheet 
public class CreateRegistrationSheetItemCommand(
    IRepository<RegistrationSheetItem, InvalidRegistrationSheetItemReason> repository)
    : CreateCommandBase<RegistrationSheetItemDto, RegistrationSheetItem, RegistrationSheetItemCreationArgs,
        InvalidRegistrationSheetItemReason>(repository)
{
    protected override async Task<RegistrationSheetItemDto> MapToDtoAsync(RegistrationSheetItem entity) => await entity.ToDto();

    protected override Task<RegistrationSheetItem> MapToEntityAsync(RegistrationSheetItemCreationArgs args) =>
        Task.FromResult(new RegistrationSheetItem
        {
            Id = args.Id,
            ArrivalDate = args.ArrivalDate,
            DepartureDate = args.DepartureDate,
            CreatorId = args.CreatorId,
            RegistrationSheetId = args.RegistrationSheetId,
        });
}