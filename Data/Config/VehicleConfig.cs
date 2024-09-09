using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");

            builder.HasKey(v => v.VehicleId);

            builder.Property(v => v.VehicleRegistrationNo).IsRequired().HasMaxLength(50);
            builder.Property(v => v.LicenseNo).IsRequired().HasMaxLength(50);
            builder.Property(v => v.LicenseExpireDate).IsRequired();
            builder.Property(v => v.VehicleColor).HasMaxLength(50);
            builder.Property(v => v.Status);

        }
    }
}



