using HotelBackend.Application.Interfaces.InterfacesDbContexts;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Application.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IHotelBackendDbContext _context;

        public ReservationRepository(IHotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Reservation>> CheckDatesAsync(
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

        public async Task<IList<Reservation>> CheckDatesAsync(
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
            Reservation reservation,
            CancellationToken cancellationToken)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return reservation.Id;
        }

        public async Task DeleteAsync(
            Reservation reservation,
            CancellationToken cancellationToken)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<Reservation>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Reservation?> GetByIdAsync(Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .AsNoTracking()
                .FirstOrDefaultAsync(reservation =>
                    reservation.Id == id);
        }

        public async Task<IList<Reservation>> GetMyAllAsync(
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
            Reservation reservation,
            CancellationToken cancellationToken)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
