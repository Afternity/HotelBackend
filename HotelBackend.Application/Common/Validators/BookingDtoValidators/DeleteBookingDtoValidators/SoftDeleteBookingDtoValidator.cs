using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.DeleteBookingDTOs;

namespace HotelBackend.Application.Common.Validators.BookingDtoValidators.DeleteBookingDtoValidators
{
    public class SoftDeleteBookingDtoValidator
        : AbstractValidator<SoftDeleteBookingDto>
    {
        public SoftDeleteBookingDtoValidator()
        {
            RuleFor(bookingDto => bookingDto.Id)
               .NotEmpty()
               .WithMessage("Id бронирования не выбрано.");
        }
    }
}
