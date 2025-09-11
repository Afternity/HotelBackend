using HotelBackend.Application.Common.Contracts.DTOs.UserDTOs;
using FluentValidation;

namespace HotelBackend.Application.Common.Contracts.Validators.UserValidators
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
            RuleFor(command => command.UserTypeId)
                .NotEqual(Guid.NewGuid());
        }
    }
}
