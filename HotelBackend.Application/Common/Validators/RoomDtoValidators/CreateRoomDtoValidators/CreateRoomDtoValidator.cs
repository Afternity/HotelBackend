using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.CreateRoomDTOs;

namespace HotelBackend.Application.Common.Validators.RoomDtoValidators.CreateRoomDtoValidators
{
    public class CreateRoomDtoValidator
        : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomDtoValidator()
        {
            RuleFor(roomDto => roomDto.Number)
                .NotEmpty()
                .WithMessage("Номер обязателен.")
                .MaximumLength(50)
                .WithMessage("Слишком длинный номер.");

            RuleFor(roomDto => roomDto.Class)
                .IsInEnum()
                .WithMessage("Недопустимый класс номера.");

            RuleFor(roomDto => roomDto.Description)
                .MinimumLength(10)
                .WithMessage("Пару слов уж надо написать.")
                .MaximumLength(200)
                .WithMessage("Описание слишком длинное.");

            RuleFor(roomDto => roomDto.PricePerNight)
                .GreaterThan(0.00m)
                .WithMessage("Цена за ночь должна быть больше 0.")
                .LessThanOrEqualTo(1000000.00m)
                .WithMessage("Цена за ночь слишком велика.");
        }
    }
}
