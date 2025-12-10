using HotelBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBackend.Persistence.Data.EntityConfigurations
{
    public class ReviewConfiguration
        : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            // ключи
            builder.HasKey(review => review.Id);

            // основные свойства
            builder.Property(review => review.Rating)
                .IsRequired(true)
                .HasColumnType("integer")
                .HasDefaultValue(5);

            builder.Property(review => review.Text)
                .IsRequired(false)
                .HasMaxLength(500);

            // метаданные
            builder.Property(review => review.CreateAt)
               .IsRequired(true)
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("now()");

            builder.Property(review => review.UpdateAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            builder.Property(review => review.IsDeleted)
                .IsRequired(true)
                .HasColumnType("boolean")
                .HasDefaultValue(false);

            builder.Property(review => review.DeletedAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            // навигационные свойства
            builder.HasOne(review => review.Booking)
                .WithMany(booking => booking.Reviews)
                .IsRequired(true)
                .HasForeignKey(review => review.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
