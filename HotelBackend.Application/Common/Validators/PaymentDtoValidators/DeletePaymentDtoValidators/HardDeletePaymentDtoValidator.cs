using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.DeletePaymentDTOs;

namespace HotelBackend.Application.Common.Validators.PaymentDtoValidators.DeletePaymentDtoValidators
{
    public class HardDeletePaymentDtoValidator
        : AbstractValidator<HardDeletePaymentDto>
    {
        public HardDeletePaymentDtoValidator()
        {
            RuleFor(paymentDto => paymentDto.Id)
                .NotEmpty()
                .WithMessage("Id платежа не выбрано.");
        }
    }
}
