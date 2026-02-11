using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using HotelBackend.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Persistence.Repositories
{
    public class BookingRepository 
        : IBookingRepository
    {
        private readonly HotelBackendDbContext _context;

        public BookingRepository(
            HotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(
            Booking booking,
            CancellationToken cancellationToken)
        {
            await _context.Bookings
                .AddAsync(booking, cancellationToken);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<Booking>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .Where(booking => booking.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<Booking>> GetAllByUserAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .Where(booking =>
                    booking.UserId == userId &&
                    booking.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }

        public async Task<Booking?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(booking =>
                    booking.Id == id,
                    cancellationToken);
        }

        public async Task<Booking?> GetLastBookingByUserAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .Where(booking => 
                    booking.UserId == userId &&
                    booking.IsDeleted == false)
                .OrderByDescending(booking => booking.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task HardDeleteAsync(
            Booking booking, 
            CancellationToken cancellationToken)
        {
            _context.Bookings
                .Remove(booking);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(
            Booking booking,
            CancellationToken cancellationToken)
        {
            _context.Bookings
                .Update(booking);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            Booking booking,
            CancellationToken cancellationToken)
        {
            _context.Bookings
                .Update(booking);
            await _context
                .SaveChangesAsync(cancellationToken);
        }
    }
}
