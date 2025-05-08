using Domain.Dtos.RegistrationSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Repository;
using Domain.RequestArgs.RegistrationSheetItems;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.RegistrationSheetItems;

public class UpdateRegistrationSheetItemCommand(
    IRepository<RegistrationSheetItem, InvalidRegistrationSheetItemReason> repository)
    : UpdateCommandBase<RegistrationSheetItemDto, RegistrationSheetItem, RegistrationSheetItemUpdateArgs,
        InvalidRegistrationSheetItemReason>(repository)
{
    protected override async Task<RegistrationSheetItemDto> MapToDtoAsync(RegistrationSheetItem entity) => await entity.ToDto();

    protected override Task ApplyUpdatesAsync(RegistrationSheetItem entity, RegistrationSheetItemUpdateArgs args)
    {
        if (args.DepartureDate is not null) entity.DepartureDate = args.DepartureDate.Value;
        if (args.ArrivalDate is not null) entity.ArrivalDate = args.ArrivalDate.Value;
        
        return Task.CompletedTask;
    }
}