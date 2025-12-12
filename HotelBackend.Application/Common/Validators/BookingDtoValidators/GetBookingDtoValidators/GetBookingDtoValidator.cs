using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.GetBookingDTOs;

namespace HotelBackend.Application.Common.Validators.BookingDtoValidators.GetBookingDtoValidators
{
    public class GetBookingDtoValidator
        : AbstractValidator<GetBookingDto>
    {
        public GetBookingDtoValidator()
        {
            RuleFor(bookingDto => bookingDto.Id)
               .NotEmpty()
               .WithMessage("Id бронирования не выбрано.");
        }
    }
}
