using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.GetBookingDTOs;

namespace HotelBackend.Application.Common.Validators.BookingDtoValidators.GetBookingDtoValidators
{
    public class GetLastUserBookingDtoValidator
        : AbstractValidator<GetLastUserBookingDto>
    {
        public GetLastUserBookingDtoValidator()
        {
            RuleFor(bookingDto => bookingDto.UserId)
             .NotEmpty()
             .WithMessage("Id пользователя не выбрано.");
        }
    }
}
