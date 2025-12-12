using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.GetReviewDTOs;

namespace HotelBackend.Application.Common.Validators.ReviewDtoValidators.GetReviewDtoValidators
{
    public class GetAllByRatingAndRoomReviewDtoValidator
         : AbstractValidator<GetAllByRatingAndRoomReviewDto>
    {
        public GetAllByRatingAndRoomReviewDtoValidator()
        {
            RuleFor(paymentDto => paymentDto.Rating)
                .GreaterThan(0)
                .WithMessage("Рейтинг не может быть меньше 0.")
                .LessThanOrEqualTo(5)
                .WithMessage("Рейтинг не может быть больше 5.");

            RuleFor(paymentDto => paymentDto.RoomId)
                .NotEmpty()
                .WithMessage("Id комнаты не выбрано.");
        }
    }
}
