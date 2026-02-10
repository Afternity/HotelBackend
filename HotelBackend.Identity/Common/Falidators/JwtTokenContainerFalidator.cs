using FluentValidation;
using HotelBackend.Shared.Contracts.Containers.IdentityContainers;

namespace HotelBackend.Identity.Common.Falidators
{
    public class JwtTokenContainerFalidator
        : AbstractValidator<JwtTokensContainer>
    {
        public JwtTokenContainerFalidator()
        {
            RuleFor(tokenModel => tokenModel.AccessToken)
                .NotEmpty()
                .WithMessage("Основной token обязателен");
            RuleFor(tokenModel => tokenModel.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token обязателен");
        }
    }
}
