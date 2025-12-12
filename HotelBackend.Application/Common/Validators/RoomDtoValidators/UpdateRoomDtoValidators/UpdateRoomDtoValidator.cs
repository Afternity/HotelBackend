using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.UpdateRoomDTOs;

namespace HotelBackend.Application.Common.Validators.RoomDtoValidators.UpdateRoomDtoValidators
{
    public class UpdateRoomDtoValidator
        : AbstractValidator<UpdateRoomDto>
    {
        public UpdateRoomDtoValidator()
        {
            RuleFor(roomDto => roomDto.Id)
                .NotEmpty()
                .WithMessage("Id комнаты не выбрано.");

            RuleFor(roomDto => roomDto.Number)
                .NotEmpty()
                .WithMessage("Номер обязателен.")
                .MaximumLength(50)
                .WithMessage("Слишком длинный номер.");

            RuleFor(roomDto => roomDto.Class)
                .IsInEnum()
                .WithMessage("Недоступный класс для номера.");

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
