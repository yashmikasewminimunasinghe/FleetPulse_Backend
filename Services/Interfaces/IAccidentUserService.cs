using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IAccidentUserService
    {
        Task<IEnumerable<AccidentUser?>> GetAllAccidentUsersAsync();
        Task<AccidentUser?> GetAccidentUserByIdAsync(int id);
        Task<bool> IsAccidentUserExist(int id);
        bool DoesAccidentUserExist(string accidentUser);
        Task<AccidentUser?> AddAccidentUserAsync(AccidentUser? accidentUser);
        Task<bool> UpdateAccidentUserAsync(AccidentUser accidentUser);
        Task DeactivateAccidentUserAsync(int accidentUserId);
        Task ActivateAccidentUserAsync(int id);
    }
}
