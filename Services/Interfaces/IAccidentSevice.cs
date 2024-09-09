using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IAccidentService
    {
        Task<IEnumerable<Accident?>> GetAllAccidentsAsync();
        Task<Accident?> GetAccidentByIdAsync(int id);
        Task<bool> IsAccidentExist(int id);
        bool DoesAccidentExists(string accident);
        Task<Accident?> AddAccidentAsync(Accident? accident);
        Task<bool> UpdateAccidentAsync(Accident accident);
        Task DeactivateAccidentAsync(int accidentId);
        Task ActivateAccidentAsync(int id);
    }
}