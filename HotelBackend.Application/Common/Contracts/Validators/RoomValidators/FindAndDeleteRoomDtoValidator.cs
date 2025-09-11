using FluentValidation;
using HotelBackend.Application.Common.Contracts.DTOs.RoomDTOs;

namespace HotelBackend.Application.Validators.RoomValidators
{
    public class FindAndDeleteRoomDtoValidator 
        : AbstractValidator<FindAndDeleteRoomDto>
    {
        public FindAndDeleteRoomDtoValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
