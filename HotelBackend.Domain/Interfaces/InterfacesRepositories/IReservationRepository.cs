using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IReservationRepository
    {
        Task<Booking?> GetByIdAsync(
            Guid Id,
            CancellationToken cancellationToken);
        Task<IList<Booking>> GetMyAllAsync(
            Guid userId,
            CancellationToken cancellationToken);
        Task<IList<Booking>> GetAllAsync(
            CancellationToken cancellationToken);
        Task<IList<Booking>> CheckDatesAsync(
            Guid roomId,
            Guid reservationId,
            DateTime checkInDate,
            DateTime checkOutDate,
            CancellationToken cancellationToken);
        Task<IList<Booking>> CheckDatesAsync(
            Guid roomId,
            DateTime checkInDate,
            DateTime checkOutDate,
            CancellationToken cancellationToken);
        Task<User?> GetGuestsById(
            Guid userId);
        Task<Guid> CreateAsync(
            Booking reservation,
            CancellationToken cancellationToken);
        Task UpdateAsync(
            Booking reservation,
            CancellationToken cancellationToken);
        Task DeleteAsync(
            Booking reservation,
            CancellationToken cancellationToken);
    }
}
