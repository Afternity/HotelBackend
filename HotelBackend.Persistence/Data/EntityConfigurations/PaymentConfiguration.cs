using HotelBackend.Domain.Enums.PaymentEnums;
using HotelBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBackend.Persistence.Data.EntityConfigurations
{
    public class PaymentConfiguration
        : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // ключи
            builder.HasKey(payment => payment.Id);

            // освновные свойства
            builder.Property(payment => payment.TotalAmount)
                .IsRequired(true)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(decimal.Zero);

            builder.Property(payment => payment.PaymentDate)
                .IsRequired(true)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(payment => payment.PaymentMethod)
                .IsRequired(true)
                .HasColumnType("integer")
                .HasConversion<int>()
                .HasDefaultValue(PaymentMethod.Card);

            builder.Property(payment => payment.Status)
                .IsRequired(true)
                .HasColumnType("integer")
                .HasConversion<int>()
                .HasDefaultValue(PaymentStatus.Paid);

            // метаданные
            builder.Property(payment => payment.CreatedAt)
               .IsRequired(true)
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("now()");

            builder.Property(payment => payment.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            builder.Property(payment => payment.IsDeleted)
                .IsRequired(true)
                .HasColumnType("boolean")
                .HasDefaultValue(false);

            builder.Property(payment => payment.DeletedAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            // навигационные свойства
            builder.HasOne(payment => payment.Booking)
                .WithOne(booking => booking.Payment)
                .IsRequired(true)
                .HasForeignKey<Payment>(payment => payment.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
