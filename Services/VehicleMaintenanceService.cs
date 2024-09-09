using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleMaintenanceService : IVehicleMaintenanceService
    {
        private readonly FleetPulseDbContext _context;

        public VehicleMaintenanceService(FleetPulseDbContext context)
        {
            _context = context;
        }
        public async Task<List<VehicleMaintenanceDTO>> GetAllVehicleMaintenancesAsync()
        {
            return await _context.VehicleMaintenances
                .Include(vm => vm.Vehicle)
                .Include(vm => vm.VehicleMaintenanceType)
                .Select(vm => new VehicleMaintenanceDTO
                {
                    MaintenanceId = vm.MaintenanceId,
                    MaintenanceDate = vm.MaintenanceDate,
                    Cost = vm.Cost,
                    PartsReplaced = vm.PartsReplaced,
                    ServiceProvider = vm.ServiceProvider,
                    SpecialNotes = vm.SpecialNotes,
                    VehicleId = vm.VehicleId,
                    VehicleRegistrationNo = vm.Vehicle.VehicleRegistrationNo,
                    VehicleMaintenanceTypeId = vm.VehicleMaintenanceTypeId,
                    TypeName = vm.VehicleMaintenanceType.TypeName,
                    Status = vm.Status,
                }).ToListAsync();
        }



        public async Task<VehicleMaintenance> GetVehicleMaintenanceByIdAsync(int MaintenanceId)
        {
            return await _context.VehicleMaintenances.FindAsync(MaintenanceId);
        }

        public async Task<VehicleMaintenance> AddVehicleMaintenanceAsync(VehicleMaintenance maintenance)
        {
            _context.VehicleMaintenances.Add(maintenance);
            await _context.SaveChangesAsync();
            return maintenance;
        }

        public async Task<Vehicle> GetByRegNoAsync(string regNo)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(c => c.VehicleRegistrationNo == regNo);
        }

        public async Task<bool> IsVehicleMaintenanceExistAsync(int id)
        {
            return await _context.VehicleMaintenances.AnyAsync(x => x.MaintenanceId == id);
        }

        public async Task<VehicleMaintenance> UpdateVehicleMaintenanceAsync(int id, VehicleMaintenance maintenance)
        {
            var existingMaintenance = await _context.VehicleMaintenances.FindAsync(id);
            if (existingMaintenance == null)
                throw new KeyNotFoundException("Vehicle Maintenance not found");

            existingMaintenance.MaintenanceDate = maintenance.MaintenanceDate;
            existingMaintenance.Cost = maintenance.Cost;
            existingMaintenance.PartsReplaced = maintenance.PartsReplaced;
            existingMaintenance.ServiceProvider = maintenance.ServiceProvider;
            existingMaintenance.SpecialNotes = maintenance.SpecialNotes;
            existingMaintenance.VehicleId = maintenance.VehicleId;
            existingMaintenance.VehicleMaintenanceTypeId = maintenance.VehicleMaintenanceTypeId;
            existingMaintenance.Status = maintenance.Status;

            _context.VehicleMaintenances.Update(existingMaintenance);
            await _context.SaveChangesAsync();

            return existingMaintenance;
        }

        public async Task DeactivateMaintenanceAsync(int maintenanceId)
        {
            var maintenance = await _context.VehicleMaintenances.FindAsync(maintenanceId);

            if (maintenance == null)
            {
                throw new InvalidOperationException("Maintenance not found.");
            }

            if (MaintenanceIsAssociatedWithVehicle(maintenance))
            {
                throw new InvalidOperationException("Maintenance is associated with a vehicle. Cannot deactivate.");
            }

            maintenance.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool MaintenanceIsAssociatedWithVehicle(VehicleMaintenance maintenance)
        {
            // Check if the maintenance is associated with any vehicle
            return _context.Vehicles.Any(v => v.VehicleMaintenances.Any(vm => vm.MaintenanceId == maintenance.MaintenanceId));
        }
    }
}
