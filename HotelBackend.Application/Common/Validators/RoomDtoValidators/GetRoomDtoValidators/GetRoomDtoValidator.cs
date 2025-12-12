using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.GetRoomDTOs;

namespace HotelBackend.Application.Common.Validators.RoomDtoValidators.GetRoomDtoValidators
{
    public class GetRoomDtoValidator
        : AbstractValidator<GetRoomDto>
    {
        public GetRoomDtoValidator()
        {
            RuleFor(roomDto => roomDto.Id)
             .NotEmpty()
             .WithMessage("Id комнаты не выбрано.");
        }
    }
}
