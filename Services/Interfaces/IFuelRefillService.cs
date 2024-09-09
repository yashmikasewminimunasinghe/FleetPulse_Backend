using FleetPulse_BackEndDevelopment.Data.DTO;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IFuelRefillService
    {
        Task<List<FuelRefillDTO>> GetAllFuelRefillsAsync();
        Task<FuelRefill> GetFuelRefillByIdAsync(int fuelRefillId);
        bool DoesFuelRefillExist(string fType);
        Task<FuelRefill?> AddFuelRefillAsync(FuelRefillDTO fuelRefillDto);
        Task<FuelRefill> UpdateFuelRefillAsync(int id, FuelRefillDTO fuelRefillDto);
        Task<bool> ActivateFuelRefillAsync(int fuelRefillId);
        Task<bool> DeactivateFuelRefillAsync(int fuelRefillId);
        Task<bool> IsFuelRefillExist(int fuelRefillId);
    }
}