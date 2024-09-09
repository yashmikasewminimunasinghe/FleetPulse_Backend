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
    public class StaffService : IStaffService
    {
        private readonly FleetPulseDbContext _context;

        public StaffService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllStaffsAsync()
        {
            return await _context.Users
                         .Where(x => x.JobTitle != null &&
                                     x.JobTitle.ToLower() != "driver" &&
                                     x.JobTitle.ToLower() != "helper")
                         .ToListAsync();
        }

        public async Task<User> GetStaffByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<bool> IsStaffExist(int id)
        {
            return Task.FromResult(_context.Users.Any(x => x.UserId == id));
        }

        public bool DoesStaffExist(string NIC)
        {
            return _context.Users.Any(x => x.NIC == NIC);
        }

        public async Task<User> AddStaffAsync(User staff)
        {
            _context.Users.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> UpdateStaffAsync(User staff)
        {
            try
            {
                _context.Users.Update(staff);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeactivateStaffAsync(int userId)
        {
            var staff = await _context.Users.FindAsync(userId);

            if (staff == null)
            {
                throw new InvalidOperationException("Staff not found.");
            }

            if (StaffIsActive(staff))
            {
                throw new InvalidOperationException("Staff is active and associated with staff details records. Cannot deactivate.");
            }

            staff.Status = false;
            await _context.SaveChangesAsync();
        }

        private bool StaffIsActive(User staff)
        {
            return _context.Users.Any(vt => vt.UserId == staff.UserId && vt.Status);
        }

        public async Task ActivateStaffAsync(int id)
        {
            var staff = await _context.Users.FindAsync(id);
            if (staff == null)
            {
                throw new KeyNotFoundException("Staff not found.");
            }

            staff.Status = true;
            await _context.SaveChangesAsync();
        }
    }
}
