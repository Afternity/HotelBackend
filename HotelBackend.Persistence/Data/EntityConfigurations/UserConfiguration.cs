using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelBackend.Domain.Models;

namespace HotelBackend.Persistence.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.Name)
                .IsUnique();
            builder.HasIndex(user => user.Email)
                .IsUnique();
            builder.HasIndex(user => user.Password)
                .IsUnique();

            builder.Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(user => user.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(user => user.UserType)
                .WithMany(userType => userType.Users)
                .IsRequired()
                .HasForeignKey(user => user.UserTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(user => user.Reservations)
                .WithOne(reservation => reservation.User)
                .IsRequired()
                .HasForeignKey(reservation => reservation.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
