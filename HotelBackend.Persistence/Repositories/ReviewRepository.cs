using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using HotelBackend.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Persistence.Repositories
{
    public class ReviewRepository
        : IReviewRepository
    {
        private readonly HotelBackendDbContext _context;

        public ReviewRepository(
            HotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(
            Review review,
            CancellationToken cancellationToken)
        {
            await _context.Reviews
                .AddAsync(review, cancellationToken);
            await _context
                .SaveChangesAsync(cancellationToken);

            return review.Id;
        }

        public async Task<IList<Review>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Reviews
                .Where(review => 
                    review.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<Review>> GetAllByRatingAndRoomAsync(
            int rating, Room room,
            CancellationToken cancellationToken)
        {
            return await _context.Reviews
                .Where(review =>
                    review.Booking.RoomId == room.Id &&
                    review.Rating == rating &&
                    review.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }

        public async Task<Review?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(review =>
                    review.Id == id,
                    cancellationToken);
        }

        public async Task HardDeleteAsync(
            Review review,
            CancellationToken cancellationToken)
        {
            _context.Reviews
                .Remove(review);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(
            Review review,
            CancellationToken cancellationToken)
        {
            _context.Reviews
                .Update(review);
            await _context
                .SaveChangesAsync (cancellationToken);
        }

        public async Task UpdateAsync(
            Review review,
            CancellationToken cancellationToken)
        {
            _context
                .Update(review);
            await _context
                .SaveChangesAsync(cancellationToken);
        }
    }
}
