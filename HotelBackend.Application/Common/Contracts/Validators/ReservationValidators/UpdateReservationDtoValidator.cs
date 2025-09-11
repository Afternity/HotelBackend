using HotelBackend.Application.Common.Contracts.DTOs.ReservationDTOs;
using FluentValidation;

namespace HotelBackend.Application.Common.Contracts.Validators.ReservationValidators
{
    public class UpdateReservationDtoValidator
        : AbstractValidator<UpdateReservationDto>
    {
        public UpdateReservationDtoValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.NewGuid());
            RuleFor(command => command.CheckInDate)
               .NotEmpty()
               .GreaterThanOrEqualTo(DateTime.Today)
               .WithMessage("get this => CheckInDate > Today");
            RuleFor(command => command.CheckOutDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("get this => ChecOutnDate > Today")
                .GreaterThan(command => command.CheckInDate)
                .WithMessage("get this => CheckOutDate > CheckInDate");
            RuleFor(command => command.GuestName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(command => command.GuestEmail)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50);
            RuleFor(command => command.RoomId)
                .NotEqual(Guid.NewGuid());
        }
    }
}
