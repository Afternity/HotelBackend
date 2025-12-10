using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.UpdateBookingDTOs;
using HotelBackend.Shared.Contracts.ViewModels.ReservationViewModes;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
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
    }
}
