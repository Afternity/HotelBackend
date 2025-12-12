using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.GetPaymentDTOs;

namespace HotelBackend.Application.Common.Validators.PaymentDtoValidators.GetPaymentDtoValidators
{
    public class GetAllByUserPaymentDtoValidator
        : AbstractValidator<GetAllByUserPaymentDto>
    {
        public GetAllByUserPaymentDtoValidator()
        {
            RuleFor(paymentDto => paymentDto.UserId)
               .NotEmpty()
               .WithMessage("Id пользователя не выбрано.");
        }
    }
}
