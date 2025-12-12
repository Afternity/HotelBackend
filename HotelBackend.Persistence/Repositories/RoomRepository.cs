using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using HotelBackend.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Persistence.Repositories
{
    public class RoomRepository 
        : IRoomRepository
    {
        private readonly HotelBackendDbContext _context;

        public RoomRepository(
            HotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(
            Room room,
            CancellationToken cancellationToken)
        {
            await _context.Rooms
                .AddAsync(room, cancellationToken);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<Room>> GetAllAsync(
            CancellationToken cancellation)
        {
            return await _context.Rooms
                .Where(room => 
                    room.IsDeleted == false)
                .ToListAsync(cancellation);
        }

        public async Task<IList<Room>> GetAllByRatingAsync(
            int rating,
            CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .Where(room =>
                    room.Reviews
                        .Any(review =>
                            review.Rating == rating))
                .ToListAsync(cancellationToken);
        }

        public async Task<Room?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .FirstOrDefaultAsync(room =>
                    room.Id == id,
                    cancellationToken);
        }

        public async Task HardDeleteAsync(
            Room room,
            CancellationToken cancellationToken)
        {
            _context.Rooms
                .Remove(room);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(
            Room room,
            CancellationToken cancellationToken)
        {
            _context.Rooms
                .Update(room);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            Room room,
            CancellationToken cancellationToken)
        {
            _context.Rooms
                .Update(room);
            await _context
                .SaveChangesAsync(cancellationToken);
        }
    }
}
