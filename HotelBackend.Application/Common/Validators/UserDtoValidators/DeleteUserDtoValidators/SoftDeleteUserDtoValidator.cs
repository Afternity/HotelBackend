using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.DeleteUserDTOs;

namespace HotelBackend.Application.Common.Validators.UserDtoValidators.DeleteUserDtoValidators
{
    public class SoftDeleteUserDtoValidator
        : AbstractValidator<SoftDeleteUserDto>
    {
        public SoftDeleteUserDtoValidator()
        {
            RuleFor(userDto => userDto.Id)
              .NotEmpty()
              .WithMessage("Id пользователя не выбрано.");
        }
    }
}
