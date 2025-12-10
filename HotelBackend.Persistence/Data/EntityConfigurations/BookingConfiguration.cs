using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelBackend.Domain.Models;

namespace HotelBackend.Persistence.Data.EntityConfigurations
{
    class BookingConfiguration 
        : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // ключи
            builder.HasKey(booking => booking.Id);

            // основные свойства
            builder.Property(booking => booking.CheckInDate)
                .IsRequired(true)
                .HasColumnType("timestamp with time zone");

            builder.Property(booking => booking.CheckOutDate)
                .IsRequired(true)
                .HasColumnType("timestamp with time zone");

            // метаданные
            builder.Property(booking => booking.CreatedAt)
               .IsRequired(true)
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("now()");

            builder.Property(booking => booking.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            builder.Property(booking => booking.IsDeleted)
                .IsRequired(true)
                .HasColumnType("boolean")
                .HasDefaultValue(false);

            builder.Property(booking => booking.DeletedAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            // навигационные свойства
            builder.HasOne(booking => booking.Room)
                .WithMany(room => room.Bookings)
                .IsRequired(true)
                .HasForeignKey(booking => booking.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(booking => booking.User)
                .WithMany(user => user.Bookings)
                .IsRequired(true)
                .HasForeignKey(booking => booking.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(booking => booking.Reviews)
                .WithOne(review => review.Booking)
                .IsRequired(true)
                .HasForeignKey(review => review.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
