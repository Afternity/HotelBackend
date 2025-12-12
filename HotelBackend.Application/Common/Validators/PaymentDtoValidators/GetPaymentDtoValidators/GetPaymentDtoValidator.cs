using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.GetPaymentDTOs;

namespace HotelBackend.Application.Common.Validators.PaymentDtoValidators.GetPaymentDtoValidators
{
    public class GetPaymentDtoValidator
        : AbstractValidator<GetPaymentDto>
    {
        public GetPaymentDtoValidator()
        {
            RuleFor(paymentDto => paymentDto.Id)
               .NotEmpty()
               .WithMessage("Id платежа не выбрано.");
        }
    }
}
