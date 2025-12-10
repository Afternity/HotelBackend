using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using HotelBackend.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Persistence.Repositories
{
    public class ReservationRepository : IBookingRepository
    {
        private readonly HotelBackendDbContext _context;

        public ReservationRepository(HotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Booking>> CheckDatesAsync(
            Guid reservationId,
            Guid roomId,
            DateTime checkInDate,
            DateTime checkOutDate,
            CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Where(reservation => 
                    reservation.Id != reservationId &&
                    reservation.RoomId == roomId &&
                    reservation.CheckInDate <= checkOutDate &&
                    reservation.CheckOutDate >= checkInDate)
                .ToListAsync(cancellationToken); 
        }

        public async Task<IList<Booking>> CheckDatesAsync(
            Guid roomId,
            DateTime checkInDate,
            DateTime checkOutDate,
            CancellationToken cancellationToken)
        {
            return await _context.Reservations
               .AsNoTracking()
                .Where(reservation =>
                    reservation.RoomId == roomId &&
                    reservation.CheckInDate <= checkOutDate &&
                    reservation.CheckOutDate >= checkInDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetGuestsById(
            Guid userId)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user =>
                    user.Id == userId);
        }

        public async Task<Guid> CreateAsync(
            Booking reservation,
            CancellationToken cancellationToken)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return reservation.Id;
        }

        public async Task DeleteAsync(
            Booking reservation,
            CancellationToken cancellationToken)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<Booking>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Booking?> GetByIdAsync(Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .AsNoTracking()
                .FirstOrDefaultAsync(reservation =>
                    reservation.Id == id);
        }

        public async Task<IList<Booking>> GetMyAllAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Where(reservations =>
                    reservations.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            Booking reservation,
            CancellationToken cancellationToken)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
