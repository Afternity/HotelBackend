using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.DeleteUserTypeDTOs;

namespace HotelBackend.Application.Common.Validators.UserTypeDtoValidators.DeleteUserTypeDtoValidators
{
    public class SoftDeleteUserTypeDtoValidator
        : AbstractValidator<SoftDeleteUserTypeDto>
    {
        public SoftDeleteUserTypeDtoValidator()
        {
            RuleFor(userTypeDto => userTypeDto.Id)
                .NotEmpty()
                .WithMessage("Id типа пользователя не выбран.");
        }
    }
}
