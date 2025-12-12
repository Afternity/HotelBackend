using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.GetReviewDTOs;

namespace HotelBackend.Application.Common.Validators.ReviewDtoValidators.GetReviewDtoValidators
{
    public class GetReviewDtoValidator
        : AbstractValidator<GetReviewDto>
    {
        public GetReviewDtoValidator()
        {
            RuleFor(reviewDto => reviewDto.Id)
                .NotEmpty()
                .WithMessage("Id отзыва не выбрано.");
        }
    }
}
