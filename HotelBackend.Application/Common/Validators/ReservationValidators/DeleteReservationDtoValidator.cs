using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.GetBookingDTOs;

namespace HotelBackend.Application.Common.Validators.ReservationValidators
{
    public class DeleteReservationDtoValidator
        : AbstractValidator<GetBookingDto>
    {
        public DeleteReservationDtoValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.NewGuid());
        }
    }
}
