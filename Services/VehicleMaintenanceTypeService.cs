using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleMaintenanceTypeService : IVehicleMaintenanceTypeService
    {
        private readonly FleetPulseDbContext _context;

        public VehicleMaintenanceTypeService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleMaintenanceType?>> GetAllVehicleMaintenanceTypesAsync()
        {
            return await _context.VehicleMaintenanceTypes.ToListAsync();
        }

        public async Task<VehicleMaintenanceType?> GetVehicleMaintenanceTypeByIdAsync(int id)
        {
            return await _context.VehicleMaintenanceTypes.FindAsync(id);
        }

        public Task<bool> IsVehicleTypeExist(int id)
        {
            return Task.FromResult(_context.VehicleMaintenanceTypes.Any(x => x.Id == id));
        }
        
        public bool DoesVehicleMaintenanceTypeExists(string vehicleMaintenanceType)
        {
            var maintenanceType = _context.VehicleMaintenanceTypes.FirstOrDefault(x => x.TypeName == vehicleMaintenanceType);
            return maintenanceType != null;
        }

        public async Task<VehicleMaintenanceType?> AddVehicleMaintenanceTypeAsync(VehicleMaintenanceType maintenanceType)
        {
            // Log the values before saving
            Console.WriteLine($"Saving TypeName: {maintenanceType.TypeName}, Status: {maintenanceType.Status}");

            _context.VehicleMaintenanceTypes.Add(maintenanceType);
            await _context.SaveChangesAsync();
            return maintenanceType;
        }

        public async Task<bool> UpdateVehicleMaintenanceTypeAsync(VehicleMaintenanceType maintenanceType)
        {
            try
            {
                var result = _context.VehicleMaintenanceTypes.Update(maintenanceType);
                result.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task DeactivateMaintenanceTypeAsync(int maintenanceTypeId)
        {
            var maintenanceType = await _context.VehicleMaintenanceTypes.FindAsync(maintenanceTypeId);

            if (maintenanceType == null)
            {
                throw new InvalidOperationException("MaintenanceType not found.");
            }

            if (MaintenanceTypeIsActive(maintenanceType))
            {
                throw new InvalidOperationException("MaintenanceType is active and associated with maintenance records. Cannot deactivate.");
            }

            maintenanceType.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool MaintenanceTypeIsActive(VehicleMaintenanceType maintenanceType)
        {
            return _context.VehicleMaintenances.Any(vm => vm.VehicleMaintenanceTypeId == maintenanceType.Id);
        }
        
        public async Task ActivateVehicleMaintenanceTypeAsync(int id)
        {
            var vehicleMaintenanceType = await _context.VehicleMaintenanceTypes.FindAsync(id);
            if (vehicleMaintenanceType == null)
            {
                throw new KeyNotFoundException("Vehicle Maintenance Type not found.");
            }

            vehicleMaintenanceType.Status = true;
            await _context.SaveChangesAsync();
        }
    }
}
