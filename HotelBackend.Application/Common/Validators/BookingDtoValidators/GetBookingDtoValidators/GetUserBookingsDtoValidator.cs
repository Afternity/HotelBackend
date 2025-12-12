using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.GetBookingDTOs;

namespace HotelBackend.Application.Common.Validators.BookingDtoValidators.GetBookingDtoValidators
{
    public class GetUserBookingsDtoValidator
        : AbstractValidator<GetUserBookingsDto>
    {
        public GetUserBookingsDtoValidator()
        {
            RuleFor(bookingDto => bookingDto.UserId)
             .NotEmpty()
             .WithMessage("Id пользователя не выбрано.");
        }
    }
}
