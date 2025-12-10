using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.UpdareUserTypeDTOs;

namespace HotelBackend.Application.Common.Validators.UserTypeValidators
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
