using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelBackend.Domain.Models;

namespace HotelBackend.Persistence.Data.EntityConfigurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.HasKey(userType => userType.Id);

            builder.HasIndex(userType => userType.Type)
                .IsUnique();

            builder.Property(userType => userType.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(userType => userType.Users)
                .WithOne(user => user.UserType)
                .IsRequired()
                .HasForeignKey(user => user.UserTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
