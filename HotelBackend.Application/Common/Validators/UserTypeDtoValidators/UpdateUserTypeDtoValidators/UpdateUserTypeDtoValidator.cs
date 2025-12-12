using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.UpdateUserTypeDTOs;

namespace HotelBackend.Application.Common.Validators.UserTypeDtoValidators.UpdateUserTypeDtoValidators
{
    public class UpdateUserTypeDtoValidator
        : AbstractValidator<UpdateUserTypeDto>
    {
        public UpdateUserTypeDtoValidator()
        {
            RuleFor(userTypeDto => userTypeDto.Id)
               .NotEmpty()
               .WithMessage("Id типа пользователя не выбран.");

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
