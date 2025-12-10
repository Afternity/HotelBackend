using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.UpdateUserDTOs;

namespace HotelBackend.Application.Common.Validators.UserValidators
{
    public class UpdateUserDtoValidator
        : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.NewGuid());
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
