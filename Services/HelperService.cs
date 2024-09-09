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
    public class HelperService : IHelperService
    {
        private readonly FleetPulseDbContext _context;

        public HelperService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllHelpersAsync()
        {
            
            return await _context.Users.Where(x => x.JobTitle != null && x.JobTitle.ToLower() == "helper").ToListAsync();
        }

        public async Task<User> GetHelperByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<bool> IsHelperExist(int id)
        {
            return Task.FromResult(_context.Users.Any(x => x.UserId == id));
        }

        public bool DoesHelperExist(string NIC)
        {
            return _context.Users.Any(x => x.NIC == NIC);
        }

        public async Task<User> AddHelperAsync(User helper)
        {
            _context.Users.Add(helper);
            await _context.SaveChangesAsync();
            return helper;
        }

        public async Task<bool> UpdateHelperAsync(User helper)
        {
            try
            {
                _context.Users.Update(helper);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeactivateHelperAsync(int userId)
        {
            var helper = await _context.Users.FindAsync(userId);

            if (helper == null)
            {
                throw new InvalidOperationException("Helper not found.");
            }

            if (HelperIsActive(helper))
            {
                throw new InvalidOperationException("Helper is active and associated with helper details records. Cannot deactivate.");
            }

            helper.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool HelperIsActive(User dhelper)
        {
            return _context.Users.Any(vt => vt.UserId == dhelper.UserId && vt.Status);
        }

        public async Task ActivateHelperAsync(int id)
        {
            var helper = await _context.Users.FindAsync(id);
            if (helper == null)
            {
                throw new KeyNotFoundException("Helper not found.");
            }

            helper.Status = true;
            await _context.SaveChangesAsync();
        }
    }
}
