using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<User>> GetAllDriversAsync();
        Task<User> GetDriverByIdAsync(int id);
        Task<bool> IsDriverExist(int id);
        bool DoesDriverExist(string NIC);
        Task<User> AddDriverAsync(User driver);
        Task<bool> UpdateDriverAsync(User driver);
        //Task DeactivateDriverAsync(int driverId);
        Task ActivateDriverAsync(int id);
        Task DeactivateDriverAsync(int userId);
    }
}
