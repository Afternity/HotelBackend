using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.DeleteRoomDTOs;

namespace HotelBackend.Application.Common.Validators.RoomDtoValidators.DeleteRoomDtoValidators
{
    public class SoftDeleteRoomDtoValidator
        : AbstractValidator<SoftDeleteRoomDto>
    {
        public SoftDeleteRoomDtoValidator()
        {
            RuleFor(roomDto => roomDto.Id)
             .NotEmpty()
             .WithMessage("Id комнаты не выбрано.");
        }
    }
}
