using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByIdAsync(
            Guid Id,
            CancellationToken cancellationToken);
        Task<IList<Reservation>> GetMyAllAsync(
            Guid userId,
            CancellationToken cancellationToken);
        Task<IList<Reservation>> GetAllAsync(
            CancellationToken cancellationToken);
        Task<IList<Reservation>> CheckDatesAsync(
            Guid roomId,
            Guid reservationId,
            DateTime checkInDate,
            DateTime checkOutDate,
            CancellationToken cancellationToken);
        Task<IList<Reservation>> CheckDatesAsync(
            Guid roomId,
            DateTime checkInDate,
            DateTime checkOutDate,
            CancellationToken cancellationToken);
        Task<User?> GetGuestsById(
            Guid userId);
        Task<Guid> CreateAsync(
            Reservation reservation,
            CancellationToken cancellationToken);
        Task UpdateAsync(
            Reservation reservation,
            CancellationToken cancellationToken);
        Task DeleteAsync(
            Reservation reservation,
            CancellationToken cancellationToken);
    }
}
