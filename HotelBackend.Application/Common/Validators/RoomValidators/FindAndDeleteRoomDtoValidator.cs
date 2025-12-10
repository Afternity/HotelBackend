using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.GetRoomDTOs;

namespace HotelBackend.Application.Common.Validators.RoomValidators
{
    public class FindAndDeleteRoomDtoValidator 
        : AbstractValidator<GetRoomDto>
    {
        public FindAndDeleteRoomDtoValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
