using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.DeleteReviewDTOs;

namespace HotelBackend.Application.Common.Validators.ReviewDtoValidators.DeleteReviewDtoValidators
{
    public class SoftDeleteReviewDtoValidator
        : AbstractValidator<SoftDeleteReviewDto>
    {
        public SoftDeleteReviewDtoValidator()
        {
            RuleFor(reviewDto => reviewDto.Id)
              .NotEmpty()
              .WithMessage("Id отзыва не выбрано.");
        }
    }
}
