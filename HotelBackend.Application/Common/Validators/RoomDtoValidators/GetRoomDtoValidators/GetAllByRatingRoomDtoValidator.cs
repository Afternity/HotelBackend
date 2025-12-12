using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.RoomDTOs.GetRoomDTOs;

namespace HotelBackend.Application.Common.Validators.RoomDtoValidators.GetRoomDtoValidators
{
    public class GetAllByRatingRoomDtoValidator
        : AbstractValidator<GetAllByRatingRoomDto>
    {
        public GetAllByRatingRoomDtoValidator()
        {
            RuleFor(roomDto => roomDto.Rating)
                .GreaterThan(0)
                .WithMessage("Рейтинг не может быть меньше 0.")
                .LessThanOrEqualTo(5)
                .WithMessage("Рейтинг не может быть больше 5.");
        }
    }
}
