using FluentValidation;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.CreatePaymentDTOs;

namespace HotelBackend.Application.Common.Validators.PaymentDtoValidators.CreatePaymentDtoValidators
{
    public class CreatePaymentDtoValidator
        : AbstractValidator<CreatePaymentDto>
    {
        public CreatePaymentDtoValidator()
        {

            RuleFor(paymentDto => paymentDto.TotalAmount)
               .GreaterThan(0.00m)
               .WithMessage("Сумма оплаты должна быть больше 0.")
               .LessThanOrEqualTo(9999999.99m)
               .WithMessage("Сумма оплаты слишком велика.");

            RuleFor(paymentDto => paymentDto.PaymentDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Дата оплаты не может быть в будущем.")
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-1))
                .WithMessage("Дата оплаты не может быть больше года назад.");

            RuleFor(paymentDto => paymentDto.PaymentMethod)
                .IsInEnum()
                .WithMessage("Недопустимый метод оплаты.");

            RuleFor(paymentDto => paymentDto.Status)
                .IsInEnum()
                .WithMessage("Недопустимый статус оплаты.");

            RuleFor(paymentDto => paymentDto.BookingId)
                .NotEmpty()
                .WithMessage("Id бронирования не выбрано.");
        }
    }
}
