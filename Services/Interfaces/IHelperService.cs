using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IHelperService
    {
        Task<IEnumerable<User>> GetAllHelpersAsync();
        Task<User> GetHelperByIdAsync(int id);
        Task<bool> IsHelperExist(int id);
        bool DoesHelperExist(string NIC);
        Task<User> AddHelperAsync(User driver);
        Task<bool> UpdateHelperAsync(User driver);
        Task DeactivateHelperAsync(int driverId);
        Task ActivateHelperAsync(int id);
    }
}
