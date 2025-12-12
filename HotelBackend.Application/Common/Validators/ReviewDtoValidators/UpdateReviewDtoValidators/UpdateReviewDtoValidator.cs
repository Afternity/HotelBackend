using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.CreateReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.UpdateReviewDTOs;
using System.Globalization;

namespace HotelBackend.Application.Common.Validators.ReviewDtoValidators.UpdateReviewDtoValidators
{
    public class UpdateReviewDtoValidator
        : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewDtoValidator()
        {
            RuleFor(reviewDto => reviewDto.Id)
                .NotEmpty()
                .WithMessage("Id отзыва не выбрано.");

            RuleFor(reviewDto => reviewDto.Text)
                .MaximumLength(500)
                .WithMessage("Текст слишком большой");

            RuleFor(reviewDto => reviewDto.Rating)
                .GreaterThan(0)
                .WithMessage("Рейтинг не может быть меньше 0")
                .LessThanOrEqualTo(5)
                .WithMessage("Рейтинг не может быть больше 5");

            RuleFor(reviewDto => reviewDto.BookingId)
                .NotEmpty()
                .WithMessage("Id бронирования не выбрано.");
        }
    }
}
