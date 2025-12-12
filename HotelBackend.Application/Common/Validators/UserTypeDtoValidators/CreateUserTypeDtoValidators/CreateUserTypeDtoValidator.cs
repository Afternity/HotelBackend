using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.CreateUserTypeDTOs;

namespace HotelBackend.Application.Common.Validators.UserTypeDtoValidators.CreateUserTypeDtoValidators
{
    public class CreateUserTypeDtoValidator
        : AbstractValidator<CreateUserTypeDto>
    {
        public CreateUserTypeDtoValidator()
        {
            RuleFor(userType => userType.Type)
                .NotEmpty()
                .WithMessage("Тип обязателен.")
                .MinimumLength(3)
                .WithMessage("Это чтобы избежать таких типов как: я, ты, он, хз и тд.")
                .MaximumLength(50)
                .WithMessage("Слишком длинный тип.");
        }
    }
}
