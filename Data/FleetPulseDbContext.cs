using FleetPulse_BackEndDevelopment.Data.Config;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Models.Configurations;
using FleetPulse_BackEndDevelopment.Models.FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data
{
    public class FleetPulseDbContext : DbContext
    {
        public FleetPulseDbContext(DbContextOptions<FleetPulseDbContext> options) : base(options)
        {
        }

        public DbSet<FuelRefill> FuelRefills { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleMaintenance> VehicleMaintenances { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripUser> TripUsers { get; set; }
        public DbSet<VehicleMaintenanceType> VehicleMaintenanceTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<FCMNotification> FCMNotifications { get; set; }
        public DbSet<VehicleMaintenanceConfiguration> VehicleMaintenanceConfigurations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<DeviceToken> DeviceTokens { get; set; }
        public DbSet<AccidentUser> AccidentUsers { get; set; }
        public DbSet<FuelRefillUser> FuelRefillUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VehicleTypeConfig());
            modelBuilder.ApplyConfiguration(new ManufactureConfig());
            modelBuilder.ApplyConfiguration(new FuelRefillConfig());
            modelBuilder.ApplyConfiguration(new VehicleConfig());
            modelBuilder.ApplyConfiguration(new AccidentConfig());
            modelBuilder.ApplyConfiguration(new AccidentUserConfig());
            modelBuilder.ApplyConfiguration(new TripConfig());
            modelBuilder.ApplyConfiguration(new TripUserConfig());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceConfig());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceTypeConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new VerificationCodeConfig());
            modelBuilder.ApplyConfiguration(new FCMNotificationConfig());

            // One-to-many relationships
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Type)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Manufacturer)
                .WithMany(m => m.Vehicles)
                .HasForeignKey(v => v.ManufactureId);


            modelBuilder.Entity<VehicleMaintenance>()
                .HasOne(vm => vm.VehicleMaintenanceType)
                .WithMany(vmt => vmt.VehicleMaintenances)
                .HasForeignKey(vm => vm.VehicleMaintenanceTypeId);

            modelBuilder.Entity<FuelRefill>()
                .HasOne(fr => fr.Vehicle)
                .WithMany(v => v.FuelRefills)
                .HasForeignKey(fr => fr.VehicleId);

            modelBuilder.Entity<FuelRefill>()
                .HasOne(fr => fr.User)
                .WithMany(u => u.FuelRefills)
                .HasForeignKey(fr => fr.UserId);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many relationships
            modelBuilder.Entity<TripUser>().HasKey(tu => new { tu.TripId, tu.UserId });

            modelBuilder.Entity<TripUser>()
                .HasOne(tu => tu.Trip)
                .WithMany(t => t.TripUsers)
                .HasForeignKey(tu => tu.TripId);

            modelBuilder.Entity<TripUser>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.TripUsers)
                .HasForeignKey(tu => tu.UserId);

            modelBuilder.Entity<VehicleMaintenance>()
                .HasOne(vm => vm.Vehicle)
                .WithMany(v => v.VehicleMaintenances)
                .HasForeignKey(vm => vm.VehicleId);
            
            modelBuilder.Entity<AccidentUser>()
                .HasOne(au => au.User)
                .WithMany(u => u.AccidentUsers)
                .HasForeignKey(au => au.UserId);
      
            modelBuilder.Entity<FuelRefillUser>()
            .HasKey(fr => new { fr.UserId, fr.FuelRefillId });

            modelBuilder.Entity<FuelRefillUser>()
                .HasOne(fr => fr.User)
                .WithMany(u => u.FuelRefillUsers)
                .HasForeignKey(fr => fr.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FuelRefillUser>()
                .HasOne(fr => fr.FuelRefill)
                .WithMany(f => f.FuelRefillUsers)
                .HasForeignKey(fr => fr.FuelRefillId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccidentUser>().HasKey(au => new { au.AccidentId, au.UserId });

            modelBuilder.Entity<AccidentUser>()
                .HasOne(au => au.Accident)
                .WithMany(a => a.AccidentUsers)
                .HasForeignKey(au => au.AccidentId);
        }
    }
}