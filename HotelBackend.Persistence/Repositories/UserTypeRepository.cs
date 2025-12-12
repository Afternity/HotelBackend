using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using HotelBackend.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Persistence.Repositories
{
    public class UserTypeRepository 
        : IUserTypeRepository
    {
        private readonly HotelBackendDbContext _context;

        public UserTypeRepository(
            HotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(
            UserType userType,
            CancellationToken cancellationToken)
        {
            await _context.UserTypes
                .AddAsync(userType, cancellationToken);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<UserType>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.UserTypes
                .AsNoTracking()
                .Where(userType => userType.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }

        public async Task<UserType?> GetByIdAsync(
            Guid id, 
            CancellationToken cancellationToken)
        {
            return await _context.UserTypes
                .FirstOrDefaultAsync(userType => 
                    userType.Id == id,
                    cancellationToken);
        }

        public async Task HardDeleteAsync(
            UserType userType, 
            CancellationToken cancellationToken)
        {
            _context.UserTypes
                .Remove(userType);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(
            UserType userType,
            CancellationToken cancellationToken)
        {
            _context.UserTypes
                .Update(userType);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            UserType userType, 
            CancellationToken cancellationToken)
        {
            _context.UserTypes
                .Update(userType);
            await _context
                .SaveChangesAsync(cancellationToken);
        }
    }
}
