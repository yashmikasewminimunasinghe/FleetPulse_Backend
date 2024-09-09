using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class ManufactureConfig : IEntityTypeConfiguration<Manufacture>
    {
        public void Configure(EntityTypeBuilder<Manufacture> builder)
        {
            builder.ToTable("Manufacture");

            builder.HasKey(x => x.ManufactureId);

            builder.Property(x => x.ManufactureId).UseIdentityColumn();
            builder.Property(n => n.ManufactureId).IsRequired();
            builder.Property(n => n.Manufacturer).IsRequired().HasMaxLength(250);
        }
    }
}