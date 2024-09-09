using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users"); // Set the table name
            
            // Set primary key
            builder.HasKey(u => u.UserId);

            // Configure properties
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.NIC).HasMaxLength(12);
            builder.Property(u => u.DriverLicenseNo).HasMaxLength(20);
            builder.Property(u => u.LicenseExpiryDate);
            builder.Property(u => u.BloodGroup).HasMaxLength(5);
            builder.Property(u => u.DateOfBirth).IsRequired();
            builder.Property(u => u.PhoneNo).HasMaxLength(15);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.HashedPassword).IsRequired().HasMaxLength(100); // Hashed passwords should be stored
            builder.Property(u => u.EmailAddress).IsRequired().HasMaxLength(100);
            builder.Property(u => u.EmergencyContact).HasMaxLength(15);
            builder.Property(u => u.JobTitle).HasMaxLength(100);
            builder.Property(u => u.ProfilePicture).HasColumnType("varbinary(max)"); // Adjust type as needed
            builder.Property(u => u.Status).IsRequired();


            
        }
    }
}
