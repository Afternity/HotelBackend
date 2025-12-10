using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.UpdateRoomDTOs;

namespace HotelBackend.Application.Common.Validators.RoomValidators
{
    public class UpdateRoomDtoValidator 
        : AbstractValidator<UpdateRoomDto>
    {
        public UpdateRoomDtoValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.NewGuid());
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
