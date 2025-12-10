using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.UpdateBookingDTOs;

namespace HotelBackend.Application.Common.Validators.ReservationValidators
{
    public class UpdateReservationDtoValidator
        : AbstractValidator<UpdateBookingDto>
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
           
            RuleFor(command => command.RoomId)
                .NotEqual(Guid.NewGuid());
        }
    }
}
