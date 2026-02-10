using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.IdentityDTOs.RegistrationDTOs;

namespace HotelBackend.Identity.Common.Falidators
{
    public class RegistrationDtoValidator
        : AbstractValidator<RegistrationDto>
    {
        public RegistrationDtoValidator()
        {
            RuleFor(registrationDto => registrationDto.Email)
                .NotEmpty()
                .WithMessage("Email обязателен.")
                .MaximumLength(100)
                .WithMessage("Email слишком длинный.")
                .EmailAddress()
                .WithMessage("Формат Email не правильный");

            RuleFor(registrationDto => registrationDto.BirthDate)
                .NotEmpty()
                .WithMessage("Дата рождения обязательна.");

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

            RuleFor(userDto => userDto.RepeatPassword)
                .NotEmpty()
                .WithMessage("Пароль нужно повторить.")
                .Must((userDto, repeatPassword) =>
                    userDto.Password == repeatPassword)
                .WithMessage("Пароли должны быть одинаковыми.");

            RuleFor(userDto => userDto.FirstName)
                .NotEmpty()
                .WithMessage("Имя обязательно.")
                .MinimumLength(2)
                .WithMessage("Имя должно иметь хотя бы 2 символа")
                .MaximumLength(100)
                .WithMessage("Имя слишком длинное.");

            RuleFor(userDto => userDto.LastName)
               .NotEmpty()
               .WithMessage("Фамилия обязательно.")
               .MinimumLength(2)
               .WithMessage("Фамилия должна иметь хотя бы 2 символа")
               .MaximumLength(100)
               .WithMessage("Фамилия слишком длинная.");

            RuleFor(userDto => userDto.MiddleName)
               .MinimumLength(2)
               .WithMessage("Отчество должно иметь хотя бы 2 символа")
               .MaximumLength(100)
               .WithMessage("Отчество слишком длинное.");

        }
    }
}
