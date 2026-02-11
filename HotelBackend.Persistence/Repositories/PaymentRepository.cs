using HotelBackend.Domain.Interfaces.InterfacesRepositories;
using HotelBackend.Domain.Models;
using HotelBackend.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Persistence.Repositories
{
    public class PaymentRepository
        : IPaymentRepository
    {
        private readonly HotelBackendDbContext _context;

        public PaymentRepository(
            HotelBackendDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(
            Payment payment,
            CancellationToken cancellationToken)
        {
            await _context.Payments
                .AddAsync(payment, cancellationToken);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<Payment>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Where(payment => 
                    payment.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<Payment>> GetAllByUserAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Where(payment => 
                    payment.Booking.UserId == userId &&
                    payment.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }

        public async Task<Payment?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(payment =>
                    payment.Id == id,
                    cancellationToken);
        }

        public async Task HardDeleteAsync(
            Payment payment,
            CancellationToken cancellationToken)
        {
            _context.Payments
                .Remove(payment);
            await _context
                .SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(
            Payment payment,
            CancellationToken cancellationToken)
        {
            _context.Payments
                .Update(payment);
            await _context 
                .SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            Payment payment,
            CancellationToken cancellationToken)
        {
            _context.Payments
                .Update(payment);
            await _context
                .SaveChangesAsync(cancellationToken);
        }
    }
}
