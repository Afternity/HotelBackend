using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.GetUserDTOs;

namespace HotelBackend.Application.Common.Validators.UserDtoValidators.GetUserDtoValidators
{
    public class GetUserDtoValidator
        : AbstractValidator<GetUserDto>
    {
        public GetUserDtoValidator()
        {
            RuleFor(userDto => userDto.Id)
              .NotEmpty()
              .WithMessage("Id пользователя не выбрано.");
        }
    }
}
