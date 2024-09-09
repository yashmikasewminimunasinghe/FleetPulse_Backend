using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IManufactureService
    {
        Task<IEnumerable<Manufacture?>> GetAllManufacturesAsync();
        Task<Manufacture?> GetManufactureByIdAsync(int id); // Correct method name
        Task<bool> IsManufactureExist(int id);
        bool DoesManufactureExists(string manufacture);
        Task<Manufacture?> AddManufactureAsync(Manufacture? manufacture);
        Task<bool> UpdateManufactureAsync(Manufacture manufacture);
        Task DeactivateManufactureAsync(int ManufactureId);
        Task ActivateManufactureAsync(int id);
    }
}
