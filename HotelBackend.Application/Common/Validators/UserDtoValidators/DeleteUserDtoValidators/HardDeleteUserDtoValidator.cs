using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.DeleteUserDTOs;

namespace HotelBackend.Application.Common.Validators.UserDtoValidators.DeleteUserDtoValidators
{
    public class HardDeleteUserDtoValidator
        : AbstractValidator<HardDeleteUserDto>
    {
        public HardDeleteUserDtoValidator()
        {
            RuleFor(userDto => userDto.Id)
              .NotEmpty()
              .WithMessage("Id пользователя не выбрано.");
        }
    }
}
