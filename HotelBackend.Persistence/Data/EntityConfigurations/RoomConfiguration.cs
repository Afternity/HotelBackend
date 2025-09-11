using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelBackend.Domain.Models;

namespace HotelBackend.Persistence.Data.EntityConfigurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(room => room.Id);

            builder.Property(room => room.Number)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(room => room.Class)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(room => room.Description)
                .HasMaxLength(200);
            builder.Property(room => room.PricePerNight)
                .IsRequired()
                .HasColumnType("numeric(18, 2)")
                .HasDefaultValue(decimal.Zero);

            builder.HasMany(room => room.Reservations)
                .WithOne(reservation => reservation.Room)
                .HasForeignKey(reservation => reservation.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
