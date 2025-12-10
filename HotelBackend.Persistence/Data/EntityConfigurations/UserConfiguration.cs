using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelBackend.Domain.Models;

namespace HotelBackend.Persistence.Data.EntityConfigurations
{
    public class UserConfiguration 
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // ключи
            builder.HasKey(user => user.Id);

            // индексы
            builder.HasIndex(u => u.Name);

            builder.HasIndex(user => user.Email)
                .IsUnique();

            // основные свойства
            builder.Property(user => user.Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasAnnotation("RegularExpression",
                    @"^[a-zA-Zа-яА-ЯёЁ\s\-']+$");

            builder.Property(user => user.Email)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasAnnotation("RegularExpression",
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            builder.Property(user => user.Password)
                .IsRequired(true)
                .HasMaxLength(256);

            // метаданные
            builder.Property(user => user.CreateAt)
              .IsRequired(true)
              .HasColumnType("timestamp with time zone")
              .HasDefaultValueSql("now()");

            builder.Property(user => user.UpdateAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            builder.Property(user => user.IsDeleted)
                .IsRequired(true)
                .HasColumnType("boolean")
                .HasDefaultValue(false);

            builder.Property(user => user.DeletedAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            // навигационные свойства
            builder.HasOne(user => user.UserType)
                .WithMany(userType => userType.Users)
                .IsRequired(true)
                .HasForeignKey(user => user.UserTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.Bookings)
                .WithOne(booking => booking.User)
                .IsRequired(true)
                .HasForeignKey(booking => booking.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
