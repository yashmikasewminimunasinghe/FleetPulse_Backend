using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class FuelRefillConfig : IEntityTypeConfiguration<FuelRefill>
    {
        public void Configure(EntityTypeBuilder<FuelRefill> builder)
        {
            builder.ToTable("FuelRefills");
            builder.HasKey(fr => fr.FuelRefillId);
            builder.Property(fr => fr.Date).IsRequired();
            builder.Property(fr => fr.Time).IsRequired();
            builder.Property(fr => fr.LiterCount).IsRequired();
            builder.Property(fr => fr.FType).HasMaxLength(50);
            builder.Property(fr => fr.Cost).HasColumnType("decimal(18, 2)");
        }
    }
}
