using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.DeleteRoomDTOs;

namespace HotelBackend.Application.Common.Validators.RoomDtoValidators.DeleteRoomDtoValidators
{
    public class HardDeleteRoomDtoValidator
        : AbstractValidator<HardDeleteRoomDto>
    {
        public HardDeleteRoomDtoValidator()
        {
            RuleFor(roomDto => roomDto.Id)
              .NotEmpty()
              .WithMessage("Id комнаты не выбрано.");
        }
    }
}
