using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.GetUserTypeDTOs;

namespace HotelBackend.Application.Common.Validators.UserTypeValidators
{
    public class FindAndDeleteUserTypeDtoValidator 
        : AbstractValidator<GetUserTypeDto>
    {
        public FindAndDeleteUserTypeDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
