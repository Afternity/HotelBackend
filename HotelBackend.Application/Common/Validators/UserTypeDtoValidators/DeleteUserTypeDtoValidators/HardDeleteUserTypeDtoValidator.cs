using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.DeleteUserTypeDTOs;

namespace HotelBackend.Application.Common.Validators.UserTypeDtoValidators.DeleteUserTypeDtoValidators
{
    public class HardDeleteUserTypeDtoValidator
        : AbstractValidator<HardDeleteUserTypeDto>
    {
        public HardDeleteUserTypeDtoValidator()
        {
            RuleFor(userTypeDto => userTypeDto.Id)
                .NotEmpty()
                .WithMessage("Id типа пользователя не выбран.");
        }
    }
}
