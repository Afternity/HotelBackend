using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IUserTypeRepository
    {
        /// <summary>
        /// Для админа
        /// </summary>
        Task<UserType?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task CreateAsync(
            UserType userType,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task UpdateAsync(
            UserType userType,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task HardDeleteAsync(
            UserType userType,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task SoftDeleteAsync(
            UserType userType,
            CancellationToken cancellationToken);

        /// <summary>
        /// Для админа
        /// </summary>
        Task<IList<UserType>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
