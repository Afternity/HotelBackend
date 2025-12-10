using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IReviewRepository
    {
        /// <summary>
        /// Для всех
        /// </summary>
        Task<Review?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для пользователя 
        /// </summary>
        Task<Guid> CreateAsync(
            Review review,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа.
        /// </summary>
        Task UpdateAsync(
            Review review,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task SoftDeleteAsync(
            Review review,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task HardDeleteAsync(
            Review review,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для пользователя
        /// </summary>
        Task<IList<Review>> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Для пользователся
        /// </summary>
        Task<IList<Review>> GetAllByRatingAndRoomAsync(
            int rating,
            Room room,
            CancellationToken cancellationToken);
    }
}
