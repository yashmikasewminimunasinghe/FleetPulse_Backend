using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly FleetPulseDbContext _context;

        public VehicleTypeService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleType?>> GetAllVehicleTypesAsync()
        {
            return await _context.VehicleTypes.ToListAsync();
        }

        public async Task<VehicleType?> GetVehicleTypeByIdAsync(int id)
        {
            return await _context.VehicleTypes.FindAsync(id);
        }

        public Task<bool> IsVehicleTypeExist(int id)
        {
            return Task.FromResult(_context.VehicleTypes.Any(x => x.VehicleTypeId == id));
        }

        public bool DoesVehicleTypeExists(string vehicleType)
        {
            var vType = _context.VehicleTypes.FirstOrDefault(x => x.Type == vehicleType);
            return vType != null;
        }

        public async Task<VehicleType?> AddVehicleTypeAsync(VehicleType? vehicleType)
        {
            _context.VehicleTypes.Add(vehicleType);
            await _context.SaveChangesAsync();
            return vehicleType;
        }



        public async Task<bool> UpdateVehicleTypeAsync(VehicleType vehicleType)
        {
            try
            {
                var result = _context.VehicleTypes.Update(vehicleType);
                result.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task DeactivateVehicleTypeAsync(int vehicleTypeId)
        {
            var vehicleType = await _context.VehicleTypes.FindAsync(vehicleTypeId);

            if (vehicleType == null)
            {
                throw new InvalidOperationException("Vehicle Type not found.");
            }

            if (VehicleTypeIsActive(vehicleType))
            {
                throw new InvalidOperationException("Vehicle Type is active and associated with type records. Cannot deactivate.");
            }

            vehicleType.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool VehicleTypeIsActive(VehicleType vehicleType)
        {
            return _context.VehicleTypes.Any(vt => vt.VehicleTypeId == vehicleType.VehicleTypeId);
        }

        public async Task ActivateVehicleTypeAsync(int id)
        {
            var vehicleType = await _context.VehicleTypes.FindAsync(id);
            if (vehicleType == null)
            {
                throw new KeyNotFoundException("Vehicle  Type not found.");
            }

            vehicleType.Status = true;
            await _context.SaveChangesAsync();
        }
    }
}