using FluentValidation;
using HotelBackend.Application.Common.Contracts.DTOs.RoomDTOs;

namespace HotelBackend.Application.Common.Contracts.Validators.RoomValidators
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
            RuleFor(command => command.Class)
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
