using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class VehicleMaintenanceConfig : IEntityTypeConfiguration<VehicleMaintenance>
    {
        public void Configure(EntityTypeBuilder<VehicleMaintenance> builder)
        {
            builder.ToTable("VehicleMaintenances");

            builder.HasKey(vm => vm.MaintenanceId);
        
            builder.Property(vm => vm.MaintenanceDate).IsRequired(); 
            builder.Property(vm => vm.Cost).HasColumnType("decimal(18, 2)");
            builder.Property(vm => vm.PartsReplaced).HasMaxLength(500);
            builder.Property(vm => vm.ServiceProvider).HasMaxLength(100);
            builder.Property(vm => vm.SpecialNotes).HasMaxLength(500);
            builder.Property(vm => vm.Status).IsRequired();
        }
    }
}
