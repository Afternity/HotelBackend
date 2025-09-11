using FluentValidation;
using HotelBackend.Application.Common.Contracts.DTOs.ReservationDTOs;

namespace HotelBackend.Application.Common.Contracts.Validators.ReservationValidators
{
    public class DeleteReservationDtoValidator
        : AbstractValidator<FindAndDeleteReservationDto>
    {
        public DeleteReservationDtoValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.NewGuid());
        }
    }
}
