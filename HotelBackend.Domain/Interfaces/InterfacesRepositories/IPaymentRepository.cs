using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IPaymentRepository
    {
        /// <summary>
        /// Для UI
        /// </summary>
        Task<Payment?> GetByIdAsync(
            Guid guid,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для пользоваателя
        /// </summary>
        Task CreateAsync(
            Payment payment,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для пользователя
        /// </summary>
        Task UpdateAsync(
            Payment payment,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для пользователя
        /// </summary>
        Task<IList<Payment>> GetAllByUserAsync(
            User user,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task SoftDeleteAsync(
            Payment payment,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task HardDeleteAsync(
            Payment payment,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task<IList<Payment>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
