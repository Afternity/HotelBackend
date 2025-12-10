using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.CreateUserDTOs;

namespace HotelBackend.Application.Common.Validators.UserValidators
{
    public class CreateUserDtoValidator
        : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(command => command.Name)
               .MaximumLength(50)
               .NotEmpty();
            RuleFor(command => command.Email)
               .MaximumLength(50)
               .NotEmpty();
            RuleFor(command => command.Password)
               .MaximumLength(50)
               .NotEmpty();
        }
    }
}
