using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class VehicleTypeConfig : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(EntityTypeBuilder<VehicleType> builder)
        {
            builder.ToTable("VehicleType");

            builder.HasKey(x => x.VehicleTypeId);

            builder.Property(x => x.VehicleTypeId).UseIdentityColumn();
            builder.Property(n => n.VehicleTypeId).IsRequired();
            builder.Property(n => n.Type).IsRequired().HasMaxLength(250);
        }
    }
}