using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelBackend.Domain.Models;

namespace HotelBackend.Persistence.Data.EntityConfigurations
{
    public class UserTypeConfiguration 
        : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            // ключи
            builder.HasKey(userType => userType.Id);

            // индексы
            builder.HasIndex(userType => userType.Type)
                .IsUnique();

            // основные свойства
            builder.Property(userType => userType.Type)
                .IsRequired(true)
                .HasMaxLength(50);

            // метаданные
            builder.Property(userType => userType.CreateAt)
              .IsRequired(true)
              .HasColumnType("timestamp with time zone")
              .HasDefaultValueSql("now()");

            builder.Property(userType => userType.UpdateAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            builder.Property(userType => userType.IsDeleted)
                .IsRequired(true)
                .HasColumnType("boolean")
                .HasDefaultValue(false);

            builder.Property(userType => userType.DeletedAt)
                .IsRequired(false)
                .HasColumnType("timestamp with time zone");

            // навигационные свойства
            builder.HasMany(userType => userType.Users)
                .WithOne(user => user.UserType)
                .IsRequired(true)
                .HasForeignKey(user => user.UserTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
