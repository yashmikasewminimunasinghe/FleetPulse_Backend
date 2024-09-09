using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly FleetPulseDbContext _context;

        public VehicleService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetAllVehiclesAsyncDisplay()
        {
            var vehicleDetails = await _context.Vehicles
                .Include(v => v.Manufacturer)
                .Include(v => v.Type)
                //.Include(v => v.FType)  // Make sure this is included if FType is a related entity
                .Select(v => new
                {
                    id = v.VehicleId,
                    VehicleRegistrationNo = v.VehicleRegistrationNo,
                    LicenseNo = v.LicenseNo,
                    LicenseExpireDate = v.LicenseExpireDate,
                    ManufacturerName = v.Manufacturer.Manufacturer,
                    typeOf = v.Type.Type,
                    typeId = v.VehicleTypeId,
                    color = v.VehicleColor,
                    status = v.Status,
                })
                .ToListAsync();

            return vehicleDetails;
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public Task<bool> IsVehicleExist(int id)
        {
            return Task.FromResult(_context.Vehicles.Any(x => x.VehicleId == id));
        }

        public bool DoesVehicleExists(string vehicleRegistrationNo)
        {
            return _context.Vehicles.Any(x => x.VehicleRegistrationNo == vehicleRegistrationNo);
        }

        public async Task<Vehicle?> AddVehicleAsync(Vehicle? vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<bool> UpdateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeactivateVehicleAsync(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                throw new InvalidOperationException("Vehicle not found.");
            }
            else
            {
                vehicle.Status = "Deactive";
                await _context.SaveChangesAsync();
            }
        }


        public async Task ActivateVehicleAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                throw new KeyNotFoundException("Vehicle not found.");
            }
            else
            {
                vehicle.Status = "Active";
                await _context.SaveChangesAsync();
            }
        }

        Task<IEnumerable<Vehicle?>> IVehicleService.GetAllVehiclesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
