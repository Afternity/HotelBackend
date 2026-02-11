using Microsoft.EntityFrameworkCore;
using HotelBackend.Domain.Models;
using HotelBackend.Persistence.Data.EntityConfigurations;

namespace HotelBackend.Persistence.Data.DbContexts
{
    public class HotelBackendDbContext : DbContext
    {
        private readonly DbContextOptions<HotelBackendDbContext> _options;

        public HotelBackendDbContext(DbContextOptions<HotelBackendDbContext> options)
            : base(options)
        {
            _options = options;
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
        }
    }
}
