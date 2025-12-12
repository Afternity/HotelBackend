using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Для всех
        /// </summary>
        Task<User?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для регистрация
        /// </summary>
        Task CreateAsync(
            User user,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для профиля
        /// </summary>
        Task UpdateAsync(
            User user,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task SoftDeleteAsync(
            User user,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task HardDeleteAsync(
            User user,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task<IList<User>> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task<IList<User>> GetAllByBookingAsync(
            CancellationToken cancellationToken);
    }
}
