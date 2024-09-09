using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class VehicleMaintenanceTypeConfig : IEntityTypeConfiguration<VehicleMaintenanceType>
    {
        public void Configure(EntityTypeBuilder<VehicleMaintenanceType> builder)
        {
            builder.ToTable("VehicleMaintenanceTypes");

            builder.HasKey(vmt => vmt.Id);

            builder.Property(vmt => vmt.TypeName).IsRequired().HasMaxLength(255);
            builder.Property(vmt => vmt.Status).IsRequired().HasDefaultValue(true);
        }
    }
}