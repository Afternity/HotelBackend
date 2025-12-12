using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using HotelBackend.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Persistence.Repositories
{
    public class UserRepository 
        : IUserRepository
    {
        private readonly HotelBackendDbContext _context;

        public UserRepository(
            HotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(
            User user,
            CancellationToken cancellationToken)
        {
            await _context.Users
                .AddAsync(user, cancellationToken);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<User>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(user =>
                    user.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<User>> GetAllByBookingAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(user => 
                    user.Bookings.Any(book => 
                        book.IsDeleted == false) &&
                    user.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user =>
                    user.Id == id,
                    cancellationToken);
        }

        public async Task HardDeleteAsync(
            User user,
            CancellationToken cancellationToken)
        {
            _context.Users
                .Remove(user);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(
            User user,
            CancellationToken cancellationToken)
        {
            _context.Users
                .Update(user);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            User user,
            CancellationToken cancellationToken)
        {
            _context.Users
                .Update(user);
            await _context
                .SaveChangesAsync(cancellationToken);
        }
    }
}
