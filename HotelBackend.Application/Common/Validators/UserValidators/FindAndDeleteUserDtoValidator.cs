using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.GetUserTypeDTOs;

namespace HotelBackend.Application.Common.Validators.UserValidators
{
    public class FindAndDeleteUserDtoValidator
        : AbstractValidator<GetUserTypeDto>
    {
        public FindAndDeleteUserDtoValidator()
        {
            RuleFor(command => command.Id)
               .NotEqual(Guid.NewGuid());
        }
    }
}
