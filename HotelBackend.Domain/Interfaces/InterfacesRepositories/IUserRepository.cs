using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
        Task<Guid> CreateAsync(
            User user,
            CancellationToken cancellationToken);
        Task UpdateAsync(
            User user,
            CancellationToken cancellationToken);
        Task DeleteAsync(
            User user,
            CancellationToken cancellationToken);
        Task<IList<User>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
