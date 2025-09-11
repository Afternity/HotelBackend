using HotelBackend.Application.Common.Contracts.DTOs.ReservationDTOs;
using HotelBackend.Application.Common.Contracts.ViewModels.ReservationViewModes;

namespace HotelBackend.Application.Features.InterfacesServices
{
    public interface IReservationService
    {
        Task<ReservationVm> GetByIdAsync(
            FindAndDeleteReservationDto findDto,
            CancellationToken cancellationToken);
        Task<ReservationMyListVm> GetMyAllAsync(
            FindAndDeleteReservationDto findDto,
            CancellationToken cancellationToken);
        Task<ReservationListVm> GetAllAsync(
            CancellationToken cancellationToken);
        Task<Guid> CreateAsync(
            CreateReservationDto createDto,
            CancellationToken cancellationToken);
        Task UpdateAsync(
            UpdateReservationDto updateDto,
            CancellationToken cancellationToken);
        Task DeleteAsync(
            FindAndDeleteReservationDto deleteDto,
            CancellationToken cancellationToken);
        Task<bool> CheckDatesAsync(
            Guid reservationId,
            Guid roomId,
            DateTime checkInDate,
            DateTime CheckOutDate,
            CancellationToken cancellationToken);
        Task<bool> CheckDatesAsync(
            Guid roomId,
            DateTime checkInDate,
            DateTime CheckOutDate,
            CancellationToken cancellationToken);
    }
}
