using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.CreateRoomDTOs;

namespace HotelBackend.Application.Common.Validators.RoomValidators
{
    public class CreateRoomDtoValidators 
        : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomDtoValidators()
        {
            RuleFor(command => command.Number)
               .MaximumLength(50)
               .NotEmpty();
            
            RuleFor(command => command.Description)
                .MaximumLength(200);
            RuleFor(command => command.PricePerNight)
                .NotEmpty()
                .PrecisionScale(18, 2, false);
        }
    }
}
