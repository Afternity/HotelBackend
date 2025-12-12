using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.UserDTOs.UpdateUserDTOs;

namespace HotelBackend.Application.Common.Validators.UserDtoValidators.UpdateUserDtoValidators
{
    public class UpdateUserDtoValidator
        : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(userDto => userDto.Id)
                .NotEmpty()
                .WithMessage("Id пользователя не выбрано.");

            RuleFor(userDto => userDto.Name)
               .NotEmpty()
               .WithMessage("Имя обязательно.")
               .MinimumLength(2)
               .WithMessage("Имя должно иметь хотя бы 2 символа")
               .MaximumLength(100)
               .WithMessage("Имя слишком длинное.");

            RuleFor(userDto => userDto.Email)
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

            RuleFor(userDto => userDto.RepeatPassword)
                .NotEmpty()
                .WithMessage("Пароль нужно повторить.")
                .Must((userDto, repeatPassword) =>
                    userDto.Password == repeatPassword)
                .WithMessage("Пароли должны быть одинаковыми.");
        }
    }
}
