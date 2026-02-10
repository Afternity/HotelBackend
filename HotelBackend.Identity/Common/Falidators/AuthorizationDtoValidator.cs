using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.IdentityDTOs.AuthorizationDTOs;

namespace HotelBackend.Identity.Common.Falidators
{
    public class AuthorizationDtoValidator
        : AbstractValidator<AuthorizationDto>
    {
        public AuthorizationDtoValidator()
        {
            RuleFor(registrationDto => registrationDto.Email)
                 .NotEmpty()
                 .WithMessage("Email обязателен.")
                 .MaximumLength(100)
                 .WithMessage("Email слишком длинный.")
                 .EmailAddress()
                 .WithMessage("Формат Email не правильный");

            RuleFor(userDto => userDto.Password)
                .NotEmpty()
                .WithMessage("Пароль обязателен.")
                .MinimumLength(8)
                .WithMessage("Пароль должен содержать хотя бы 8 символов.")
                .MaximumLength(256)
                .WithMessage("Пароль слишком длинный.")
                .Matches(@"[A-Z]+")
                .WithMessage("Пароль должен содержать хотя бы одну заглавную букву.")
                .Matches(@"[a-z]+")
                .WithMessage("Пароль должен содержать хотя бы одну строчную букву.")
                .Matches(@"[0-9]+")
                .WithMessage("Пароль должен содержать хотя бы одну цифру.");

        }
    }
}
