using HotelBackend.Application.Interfaces.InterfacesDbContexts;
using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Application.Repositories
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly IHotelBackendDbContext _context;

        public UserTypeRepository(
            IHotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(
            UserType userType,
            CancellationToken cancellationToken)
        {
            await _context.UserTypes.AddAsync(userType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return userType.Id;
        }

        public async Task DeleteAsync(
            UserType userType,
            CancellationToken cancellationToken)
        {
            _context.UserTypes.Remove(userType);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<UserType>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.UserTypes
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<UserType?> GetByIdAsync(
            Guid id, 
            CancellationToken cancellationToken)
        {
            return await _context.UserTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(userType => userType.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(
            UserType userType, 
            CancellationToken cancellationToken)
        {
            _context.UserTypes.Update(userType);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
