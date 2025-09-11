using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelBackend.Domain.Models;

namespace HotelBackend.Persistence.Data.EntityConfigurations
{
    class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(reservation => reservation.Id);

            builder.Property(reservation => reservation.CheckInDate)
                .IsRequired()
                .HasColumnType("timestamp with time zone");
            builder.Property(reservation => reservation.CheckOutDate)
                .IsRequired()
                .HasColumnType("timestamp with time zone");
            builder.Property(reservation => reservation.GuestName)
                .HasMaxLength(50);
            builder.Property(reservation => reservation.GuestEmail)
                .HasMaxLength(50);

            builder.HasOne(reservation => reservation.Room)
                .WithMany(room => room.Reservations)
                .HasForeignKey(reservation => reservation.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(reservation => reservation.User)
                .WithMany(user => user.Reservations)
                .HasForeignKey(reservation => reservation.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
