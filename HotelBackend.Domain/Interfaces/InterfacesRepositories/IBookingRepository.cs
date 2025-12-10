using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IBookingRepository
    {
        /// <summary>
        /// Для UI
        /// </summary>
        Task<Booking?> GetByIdAsync(
            Guid Id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для пользователя
        /// </summary>
        Task<Guid> CreateAsync(
            Booking booking,
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Больше для админа чем для пользователя
        /// </summary>
        Task UpdateAsync(
            Booking booking,
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Больше для админа чем для пользователя
        /// </summary>
        Task SoftDeleteAsync(
           Booking booking,
           CancellationToken cancellationToken);
        
        /// <summary>
        /// Только для админа
        /// </summary>
        Task HardDeleteAsync(
            Booking booking,
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Для UI
        /// </summary>
        Task<IList<Booking>> GetAllAsync(
            CancellationToken cancellationToken);
       
        /// <summary>
        /// Для пользователя
        /// </summary>
        Task<IList<Booking>> GetAllByUserAsync(
            User user,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для пользователя
        /// </summary>
        Task<Booking?> GetLastBookingByUser(
            User user,
            CancellationToken cancellationToken);
    }
}
