using FluentValidation;
using HotelBackend.Application.Common.Contracts.DTOs.ReservationDTOs;

namespace HotelBackend.Application.Common.Validators.ReservationValidators
{
    public class CreateReservationDtoValidator
        : AbstractValidator<CreateReservationDto>
    {
        public CreateReservationDtoValidator()
        {
            RuleFor(command => command.CheckInDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("get this => CheckInDate > Today");
            RuleFor(command => command.CheckOutDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("get this => CheckOutDate > Today")
                .GreaterThan(command => command.CheckInDate)
                .WithMessage("get this => CheckOutDate > CheckInDate");
            RuleFor(command => command.UserId)
                .NotEqual(Guid.NewGuid());
            RuleFor(command => command.RoomId)
                .NotEqual(Guid.NewGuid());
        }
    }
}
