using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle?>> GetAllVehiclesAsync();
        Task<object> GetAllVehiclesAsyncDisplay();
        Task<Vehicle?> GetVehicleByIdAsync(int id);
        Task<bool> IsVehicleExist(int id);
        bool DoesVehicleExists(string vehicleRegistrationNo);
        Task<Vehicle?> AddVehicleAsync(Vehicle? vehicle);
        Task<bool> UpdateVehicleAsync(Vehicle vehicle);
        Task DeactivateVehicleAsync(int vehicleId);
        Task ActivateVehicleAsync(int id);
    }
}
