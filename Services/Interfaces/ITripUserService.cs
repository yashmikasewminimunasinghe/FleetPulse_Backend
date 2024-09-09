using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface ITripUserService
    {
        Task<IEnumerable<TripUser>> GetAllTripUsersAsync();
        Task<TripUser> GetTripUserByIdAsync(int id);
        Task<bool> IsTripUserExistAsync(int id);
        Task<bool> DoesTripUserExistAsync(string tripUserId);
        Task<TripUser> AddTripUserAsync(TripUser tripUser);
        Task<bool> UpdateTripUserAsync(TripUser tripUser);
        Task DeactivateTripUserAsync(int tripUserId);
        Task ActivateTripUserAsync(int id);
    }
}
