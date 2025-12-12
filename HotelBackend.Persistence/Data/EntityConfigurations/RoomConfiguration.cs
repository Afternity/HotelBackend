using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelBackend.Domain.Models;
using HotelBackend.Domain.Enums.RoomEnums;

namespace HotelBackend.Persistence.Data.EntityConfigurations
{
    public class RoomConfiguration 
        : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            // ключи
            builder.HasKey(room => room.Id);

            // индексы
            builder.HasIndex(room => room.Number)
                .IsUnique();

            // основные свойства
            builder.Property(room => room.Number)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(room => room.Class)
                .IsRequired(true)
                .HasColumnType("integer")
                .HasConversion<int>()
                .HasDefaultValue(RoomClass.Standard);

            builder.Property(room => room.Description)
                .IsRequired(true)
                .HasMaxLength(200);

            builder.Property(room => room.PricePerNight)
                .IsRequired(true)
                .HasColumnType("numeric(18, 2)")
                .HasDefaultValue(decimal.Zero);

            // метаданные
            builder.Property(room => room.CreatedAt)
              .IsRequired(true)
              .HasColumnType("timestamp with time zone")
              .HasDefaultValueSql("now()");

            builder.Property(room => room.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            builder.Property(room => room.IsDeleted)
                .IsRequired(true)
                .HasColumnType("boolean")
                .HasDefaultValue(false);

            builder.Property(room => room.DeletedAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            // навигационные свойства
            builder.HasMany(room => room.Bookings)
                .WithOne(booking => booking.Room)
                .IsRequired(true) 
                .HasForeignKey(booking => booking.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
