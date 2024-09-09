using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class AccidentUserService : IAccidentUserService
    {
        private readonly FleetPulseDbContext _context;

        public AccidentUserService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccidentUser?>> GetAllAccidentUsersAsync()
        {
            return await _context.AccidentUsers.ToListAsync();
        }

        public async Task<AccidentUser?> GetAccidentUserByIdAsync(int id)
        {
            return await _context.AccidentUsers.FindAsync(id);
        }

        public Task<bool> IsAccidentUserExist(int id)
        {
            return Task.FromResult(_context.AccidentUsers.Any(x => x.UserId == id));
        }

        public bool DoesAccidentUserExist(string accidentUserName)
        {
            return _context.AccidentUsers.Any(x => x.User.UserName == accidentUserName); // Assuming 'UserName' is a property of User
        }

        public async Task<AccidentUser?> AddAccidentUserAsync(AccidentUser? accidentUser)
        {
            if (accidentUser != null)
            {
                _context.AccidentUsers.Add(accidentUser);
                await _context.SaveChangesAsync();
            }
            return accidentUser;
        }

        public async Task<bool> UpdateAccidentUserAsync(AccidentUser accidentUser)
        {
            var existingAccidentUser = await _context.AccidentUsers.FindAsync(accidentUser.UserId);
            if (existingAccidentUser != null)
            {
                _context.Entry(existingAccidentUser).CurrentValues.SetValues(accidentUser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeactivateAccidentUserAsync(int accidentUserId)
        {
            var accidentUser = await _context.AccidentUsers.FindAsync(accidentUserId);

            if (accidentUser == null)
            {
                throw new InvalidOperationException("Accident User not found.");
            }

            if (AccidentUserIsActive(accidentUser))
            {
                throw new InvalidOperationException("Accident user is active and associated with accident user records. Cannot deactivate.");
            }

            accidentUser.Status = false; // Assuming 'Status' is a boolean property indicating active/inactive
            await _context.SaveChangesAsync();
        }

        private bool AccidentUserIsActive(AccidentUser accidentUser)
        {
            return accidentUser.Status; // Assuming 'Status' is a boolean property indicating active/inactive
        }

        public async Task ActivateAccidentUserAsync(int id)
        {
            var accidentUser = await _context.AccidentUsers.FindAsync(id);
            if (accidentUser == null)
            {
                throw new KeyNotFoundException("Accident user not found.");
            }

            accidentUser.Status = true; // Assuming 'Status' is a boolean property indicating active/inactive
            await _context.SaveChangesAsync();
        }
    }
}
