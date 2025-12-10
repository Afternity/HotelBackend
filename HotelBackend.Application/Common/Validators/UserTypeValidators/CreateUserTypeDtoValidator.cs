using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.CreateUserTypeDTOs;

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
