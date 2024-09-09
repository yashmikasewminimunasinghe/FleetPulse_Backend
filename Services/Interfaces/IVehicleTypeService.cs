using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IVehicleTypeService
    {
        Task<IEnumerable<VehicleType?>> GetAllVehicleTypesAsync();
        Task<VehicleType?> GetVehicleTypeByIdAsync(int id);
        Task<bool> IsVehicleTypeExist(int id);
        bool DoesVehicleTypeExists(string vehicleType);
        Task<VehicleType?> AddVehicleTypeAsync(VehicleType? vehicleType);
        Task<bool> UpdateVehicleTypeAsync(VehicleType vehicleType);
        Task DeactivateVehicleTypeAsync(int vehicleTypeId);
        Task ActivateVehicleTypeAsync(int id);
    }
}