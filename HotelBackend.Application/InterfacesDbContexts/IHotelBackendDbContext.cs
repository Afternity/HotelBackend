using Microsoft.EntityFrameworkCore;
using HotelBackend.Domain.Models;

namespace HotelBackend.Application.Interfaces.InterfacesDbContexts
{
    public interface IHotelBackendDbContext
    {
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserType> UserTypes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
