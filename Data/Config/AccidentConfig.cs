using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class AccidentConfig : IEntityTypeConfiguration<Accident>
    {
        public void Configure(EntityTypeBuilder<Accident> builder)
        {
            builder.ToTable("Accidents");

            builder.HasKey(a => a.AccidentId);

            builder.Property(a => a.Venue).IsRequired().HasMaxLength(100);
            builder.Property(a => a.DateTime).IsRequired();
            builder.Property(a => a.Photos).HasMaxLength(500);
            builder.Property(a => a.SpecialNotes).HasMaxLength(500);
            builder.Property(a => a.Loss).HasColumnType("decimal(18, 2)");
            builder.Property(a => a.DriverInjuredStatus).IsRequired();
            builder.Property(a => a.HelperInjuredStatus).IsRequired();
            builder.Property(a => a.VehicleDamagedStatus).IsRequired();
            builder.Property(a => a.Status).IsRequired();
        }
    }
}