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
    public class TripService : ITripService
    {
        private readonly FleetPulseDbContext _context;

        public TripService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip>> GetAllTripsAsync()
        {
            return await _context.Trips.ToListAsync();
        }

        public async Task<Trip> GetTripByIdAsync(int id)
        {
            return await _context.Trips.FindAsync(id);
        }

        public async Task<bool> IsTripExist(int id)
        {
            return await _context.Trips.AnyAsync(x => x.TripId == id.ToString());
        }

        public bool DoesTripExists(string tripId)
        {
            return _context.Trips.Any(x => x.TripId == tripId);
        }

        public async Task<Trip> AddTripAsync(Trip trip)
        {
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return trip;
        }

        public async Task<bool> UpdateTripAsync(Trip trip)
        {
            try
            {
                _context.Trips.Update(trip);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeactivateTripAsync(int tripId)
        {
            var trip = await _context.Trips.FindAsync(tripId);

            if (trip == null)
            {
                throw new InvalidOperationException("Trip not found.");
            }

            if (TripIsActive(trip))
            {
                throw new InvalidOperationException("Trip is active and associated with trip records. Cannot deactivate.");
            }

            trip.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool TripIsActive(Trip trip)
        {
            return _context.Trips.Any(vt => vt.TripId == trip.TripId && vt.Status);
        }

        public async Task ActivateTripAsync(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                throw new KeyNotFoundException("Trip not found.");
            }

            trip.Status = true;
            await _context.SaveChangesAsync();
        }
    }
}
