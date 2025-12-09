using Microsoft.EntityFrameworkCore;
using HotelBackend.Application.Interfaces.InterfacesDbContexts;
using HotelBackend.Domain.Models;
using HotelBackend.Persistence.Data.EntityConfigurations;

namespace HotelBackend.Persistence.Data.DbContexts
{
    public class HotelBackendDbContext : DbContext, IHotelBackendDbContext
    {
        private readonly DbContextOptions<HotelBackendDbContext> _options;

        public HotelBackendDbContext(DbContextOptions<HotelBackendDbContext> options)
            : base(options)
        {
            _options = options;
        }

        public DbSet<Booking> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        }
    }
}
