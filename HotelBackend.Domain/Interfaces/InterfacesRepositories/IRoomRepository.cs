using HotelBackend.Domain.Models;

namespace HotelBackend.Domain.Interfaces.InterfacesRepositories
{
    public interface IRoomRepository
    {
        Task<Room?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
        Task<Guid> CreateAsync(
            Room room,
            CancellationToken cancellationToken);
        Task UpdateAsync(
            Room room,
            CancellationToken cancellationToken);
        Task DeleteAsync(
            Room room,
            CancellationToken cancellationToken);
        Task<IList<Room>> GetAllAsync(
            CancellationToken cancellation);
    }
}
