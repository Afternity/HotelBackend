using HotelBackend.Application.Common.Contracts.DTOs.UserDTOs;
using FluentValidation;

namespace HotelBackend.Application.Common.Contracts.Validators.UserValidators
{
    public class FindAndDeleteUserDtoValidator
        : AbstractValidator<FindAndDeleteUserDto>
    {
        public FindAndDeleteUserDtoValidator()
        {
            RuleFor(command => command.Id)
               .NotEqual(Guid.NewGuid());
        }
    }
}
