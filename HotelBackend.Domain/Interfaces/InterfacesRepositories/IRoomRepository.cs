using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IRoomRepository
    {
        /// <summary>
        /// Для всех
        /// </summary>
        Task<Room?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task CreateAsync(
            Room room,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task UpdateAsync(
            Room room,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task SoftDeleteAsync(
            Room room,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task HardDeleteAsync(
            Room room,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для пользователя
        /// </summary>
        Task<IList<Room>> GetAllAsync(
            CancellationToken cancellation);

        /// <summary>
        /// Для пользователя
        /// </summary>
        Task<IList<Room>> GetAllByRatingAsync(
            int rating,
            CancellationToken cancellationToken);
    }
}
