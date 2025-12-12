using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.DeleteReviewDTOs;

namespace HotelBackend.Application.Common.Validators.ReviewDtoValidators.DeleteReviewDtoValidators
{
    public class HardDeleteReviewDtoValidator
        : AbstractValidator<HardDeleteReviewDto>
    {
        public HardDeleteReviewDtoValidator()
        {
            RuleFor(reviewDto => reviewDto.Id)
              .NotEmpty()
              .WithMessage("Id отзыва не выбрано.");
        }
    }
}
