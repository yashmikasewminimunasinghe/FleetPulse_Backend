using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class TripUserService : ITripUserService
    {
        private readonly FleetPulseDbContext _context;

        public TripUserService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TripUser>> GetAllTripUsersAsync()
        {
            return await _context.TripUsers.ToListAsync();
        }

        public async Task<TripUser> GetTripUserByIdAsync(int id)
        {
            return await _context.TripUsers.FindAsync(id);
        }

        public async Task<bool> IsTripUserExistAsync(int id)
        {
            return await _context.TripUsers.AnyAsync(x => x.TripUserId == id);
        }

        public async Task<bool> DoesTripUserExistAsync(string tripUserId)
        {
            return await _context.TripUsers.AnyAsync(x => x.TripUserId.ToString() == tripUserId);
        }

        public async Task<TripUser> AddTripUserAsync(TripUser tripUser)
        {
            _context.TripUsers.Add(tripUser);
            await _context.SaveChangesAsync();
            return tripUser;
        }

        public async Task<bool> UpdateTripUserAsync(TripUser tripUser)
        {
            try
            {
                _context.TripUsers.Update(tripUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeactivateTripUserAsync(int tripUserId)
        {
            var tripUser = await _context.TripUsers.FindAsync(tripUserId);

            if (tripUser == null)
            {
                throw new InvalidOperationException("Trip User not found.");
            }

            if (TripUserIsActive(tripUser))
            {
                throw new InvalidOperationException("Trip User is active and associated with trip user records. Cannot deactivate.");
            }

            tripUser.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool TripUserIsActive(TripUser tripUser)
        {
            return _context.TripUsers.Any(vt => vt.TripUserId == tripUser.TripUserId && vt.Status);
        }

        public async Task ActivateTripUserAsync(int id)
        {
            var tripUser = await _context.TripUsers.FindAsync(id);
            if (tripUser == null)
            {
                throw new KeyNotFoundException("Trip User not found.");
            }

            tripUser.Status = true;
            await _context.SaveChangesAsync();
        }
    }
}
