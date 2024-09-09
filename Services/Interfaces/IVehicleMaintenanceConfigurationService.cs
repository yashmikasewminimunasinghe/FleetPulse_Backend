using FleetPulse_BackEndDevelopment.Data.DTO;

namespace FleetPulse_BackEndDevelopment.Services
{
    public interface IVehicleMaintenanceConfigurationService
    {
        Task<VehicleMaintenanceConfigurationDTO> AddVehicleMaintenanceConfigurationAsync(VehicleMaintenanceConfigurationDTO dto);
        Task<VehicleMaintenanceConfigurationDTO> GetVehicleMaintenanceConfigurationByIdAsync(int id);
        Task<IEnumerable<VehicleMaintenanceConfigurationDTO>> GetAllVehicleMaintenanceConfigurationsAsync();
        Task<bool> UpdateVehicleMaintenanceConfigurationAsync(VehicleMaintenanceConfigurationDTO dto);
        Task<bool> DeleteVehicleMaintenanceConfigurationAsync(int id);
        Task<List<VehicleMaintenanceConfigurationDTO>> GetDueMaintenanceTasksAsync();
    }
}
