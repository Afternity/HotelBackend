using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.CreateBookingDTOs;

namespace HotelBackend.Application.Common.Validators.BookingDtoValidators.CreateBookingDtoValidators
{
    /// <summary>
    /// В будующем можно добавить consts для дней, улучшить modelDto, сделать ещё проверки
    /// </summary>
    public class CreateBookingDtoValidator
        : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingDtoValidator() 
        {
            // даты 
            RuleFor(bookingDto => bookingDto.CheckInDate)
                .NotEmpty()
                .WithMessage("Выбор даты заезда обязателен.")
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("Дата заезда не должна быть в прошлом.");

            RuleFor(bookingDto => bookingDto.CheckOutDate)
                .NotEmpty()
                .WithMessage("Выбор даты выезда обязателен.")
                .GreaterThan(bookingDto => bookingDto.CheckInDate)
                .WithMessage("Дата выезда должна быть позже даты заезда.")
                .Must((bookiongDto, checkOutDate) =>
                    (checkOutDate - bookiongDto.CheckInDate).TotalDays >= 1)
                .WithMessage("Минимальная продолжительность бронирования - 1 день")
                .Must((bookingDto, checkOutDate) =>
                    (checkOutDate - bookingDto.CheckInDate).TotalDays <= 30)
                .WithMessage("Максимальная продолжительность бронирования - 30 дней");

            // навиигационные поля
            RuleFor(bookingDto => bookingDto.RoomId)
                .NotEmpty()
                .WithMessage("Id комнаты не выбрано.");

            RuleFor(bookingDto => bookingDto.UserId)
                .NotEmpty()
                .WithMessage("Id пользователя не выбрано.");
        }
    }
}
