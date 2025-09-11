using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IUserTypeRepository
    {
        Task<UserType?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
        Task<Guid> CreateAsync(
            UserType userType,
            CancellationToken cancellationToken);
        Task UpdateAsync(
            UserType userType,
            CancellationToken cancellationToken);
        Task DeleteAsync(
            UserType userType,
            CancellationToken cancellationToken);
        Task<IList<UserType>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
