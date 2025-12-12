using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.GetUserTypeDTOs;

namespace HotelBackend.Application.Common.Validators.UserTypeDtoValidators.GetUserTypeDtoValidators
{
    public class GetUserTypeDtoValidator
        : AbstractValidator<GetUserTypeDto>
    {
        public GetUserTypeDtoValidator()
        {
            RuleFor(userTypeDto => userTypeDto.Id)
                .NotEmpty()
                .WithMessage("Id типа пользователя не выбран.");
        }
    }
}
