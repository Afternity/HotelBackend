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
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        }
    }
}
