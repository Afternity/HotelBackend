using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using HotelBackend.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelBackendDbContext _context;

        public BookingRepository(HotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetGuestsById(
            Guid userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user =>
                    user.Id == userId);
        }

        public async Task<Guid> CreateAsync(
            Booking reservation,
            CancellationToken cancellationToken)
        {
            await _context.Bookings.AddAsync(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return reservation.Id;
        }

        public async Task DeleteAsync(
            Booking reservation,
            CancellationToken cancellationToken)
        {
            _context.Bookings.Remove(reservation);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<Booking>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Booking?> GetByIdAsync(Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(reservation =>
                    reservation.Id == id);
        }

        public async Task<IList<Booking>> GetMyAllAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(reservations =>
                    reservations.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            Booking reservation,
            CancellationToken cancellationToken)
        {
            _context.Bookings.Update(reservation);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task SoftDeleteAsync(Booking booking, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task HardDeleteAsync(Booking booking, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Booking>> GetAllByUserAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Booking?> GetLastBookingByUser(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
