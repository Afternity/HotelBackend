using HotelBackend.Application.Common.Contracts.DTOs.UserTypeDTOs;
using FluentValidation;

namespace HotelBackend.Application.Common.Validators.UserTypeValidators
{
    public class CreateUserTypeDtoValidator
        : AbstractValidator<CreateUserTypeDto>
    {
        public CreateUserTypeDtoValidator()
        {
            RuleFor(dto => dto.Type)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
