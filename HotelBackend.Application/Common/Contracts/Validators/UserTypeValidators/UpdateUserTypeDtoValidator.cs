using FluentValidation;
using HotelBackend.Application.Common.Contracts.DTOs.UserTypeDTOs;

namespace HotelBackend.Application.Common.Contracts.Validators.UserTypeValidators
{
    public class UpdateUserTypeDtoValidator
        : AbstractValidator<UpdateUserTypeDto>
    {
        public UpdateUserTypeDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEqual(Guid.Empty);

            RuleFor(dto => dto.Type)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
