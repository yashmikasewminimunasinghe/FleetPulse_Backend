using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IVehicleMaintenanceTypeService
    {
        Task<IEnumerable<VehicleMaintenanceType?>> GetAllVehicleMaintenanceTypesAsync();
        Task<VehicleMaintenanceType?> GetVehicleMaintenanceTypeByIdAsync(int id);
        Task<VehicleMaintenanceType?> AddVehicleMaintenanceTypeAsync(VehicleMaintenanceType? maintenanceType); 
        bool DoesVehicleMaintenanceTypeExists(string vehicleMaintenanceType);
        Task<bool> UpdateVehicleMaintenanceTypeAsync(VehicleMaintenanceType maintenanceType);
        Task DeactivateMaintenanceTypeAsync(int maintenanceTypeId);
        Task<bool> IsVehicleTypeExist(int id);
        Task ActivateVehicleMaintenanceTypeAsync(int id);

    }
}