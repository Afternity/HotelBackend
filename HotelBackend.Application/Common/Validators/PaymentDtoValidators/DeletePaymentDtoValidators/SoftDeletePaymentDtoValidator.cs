using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.DeletePaymentDTOs;

namespace HotelBackend.Application.Common.Validators.PaymentDtoValidators.DeletePaymentDtoValidators
{
    public class SoftDeletePaymentDtoValidator
        : AbstractValidator<SoftDeletePaymentDto>
    {
        public SoftDeletePaymentDtoValidator()
        {
            RuleFor(paymentDto => paymentDto.Id)
               .NotEmpty()
               .WithMessage("Id платежа не выбрано.");
        }
    }
}
