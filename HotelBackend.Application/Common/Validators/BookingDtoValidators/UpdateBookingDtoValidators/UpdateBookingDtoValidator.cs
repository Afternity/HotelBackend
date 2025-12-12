using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.UpdateBookingDTOs;

namespace HotelBackend.Application.Common.Validators.BookingDtoValidators.UpdateBookingDtoValidators
{
    public class UpdateBookingDtoValidator
        : AbstractValidator<UpdateBookingDto>
    {
        public UpdateBookingDtoValidator()
        {
            // ключи
            RuleFor(bookingDto => bookingDto.Id)
                .NotEmpty()
                .WithMessage("Id бронирования не выбрано.");

            // даты 
            RuleFor(bookingDto => bookingDto.CheckInDate)
                .NotEmpty()
                .WithMessage("Выбор даты заезда обязателен.");

            RuleFor(bookingDto => bookingDto.CheckOutDate)
                .NotEmpty()
                .WithMessage("Выбор дыты выезда обязателен.")
                .GreaterThan(bookingDto => bookingDto.CheckInDate)
                .WithMessage("Дата выезада должна быть позже даты заезда.")
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
