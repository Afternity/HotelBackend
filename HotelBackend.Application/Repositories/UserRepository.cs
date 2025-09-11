using HotelBackend.Application.Interfaces.InterfacesDbContexts;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHotelBackendDbContext _context;

        public UserRepository(
            IHotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(
            User user,
            CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        public async Task DeleteAsync(
            User user,
            CancellationToken cancellationToken)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<User>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user =>
                    user.Id == id,
                    cancellationToken);
        }

        public async Task UpdateAsync(
            User user,
            CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
