using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface ITripService
    {
        Task<IEnumerable<Trip>> GetAllTripsAsync();
        Task<Trip> GetTripByIdAsync(int id);
        Task<bool> IsTripExist(int id);
        bool DoesTripExists(string tripId);
        Task<Trip> AddTripAsync(Trip trip);
        Task<bool> UpdateTripAsync(Trip trip);
        Task DeactivateTripAsync(int tripId);
        Task ActivateTripAsync(int id);
    }
}
