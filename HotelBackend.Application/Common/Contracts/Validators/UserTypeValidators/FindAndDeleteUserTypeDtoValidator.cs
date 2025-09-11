using HotelBackend.Application.Common.Contracts.DTOs.UserTypeDTOs;
using FluentValidation;

namespace HotelBackend.Application.Common.Contracts.Validators.UserTypeValidators
{
    public class FindAndDeleteUserTypeDtoValidator 
        : AbstractValidator<FindAndDeleteUserTypeDto>
    {
        public FindAndDeleteUserTypeDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
