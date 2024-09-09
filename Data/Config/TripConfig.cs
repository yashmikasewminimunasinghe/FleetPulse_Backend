using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.ToTable("Trips");

            builder.HasKey(t => t.TripId);

            builder.Property(t => t.Date).IsRequired();
            builder.Property(t => t.StartTime).IsRequired();
            builder.Property(t => t.EndTime);
            builder.Property(t => t.StartMeterValue).IsRequired();
            builder.Property(t => t.EndMeterValue);
            builder.Property(t => t.Status).IsRequired();
        }
    }
}