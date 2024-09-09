using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<User>> GetAllStaffsAsync();
        Task<User> GetStaffByIdAsync(int id);
        Task<bool> IsStaffExist(int id);
        bool DoesStaffExist(string NIC);
        Task<User> AddStaffAsync(User staff);
        Task<bool> UpdateStaffAsync(User staff);
        Task DeactivateStaffAsync(int Id);
        Task ActivateStaffAsync(int userId);
    }
}
