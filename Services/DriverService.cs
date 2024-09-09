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
    public class DriverService : IDriverService
    {
        private readonly FleetPulseDbContext _context;

        public DriverService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllDriversAsync()
        {
            return await _context.Users.Where(x=> x.JobTitle != null && x.JobTitle.ToLower() == "driver").ToListAsync();
        }

        public async Task<User> GetDriverByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<bool> IsDriverExist(int id)
        {
            return Task.FromResult(_context.Users.Any(x => x.UserId == id));
        }

        public bool DoesDriverExist(string NIC)
        {
            return _context.Users.Any(x => x.NIC == NIC);
        }

        public async Task<User> AddDriverAsync(User driver)
        {
            try
            {
                _context.Users.Add(driver);
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                
            }
            return driver;
        }

        public async Task<bool> UpdateDriverAsync(User driver)
        {
            try
            {
                _context.Users.Update(driver);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task ActivateDriverAsync(int id)
        {
            var driver = await _context.Users.FindAsync(id);
            if (driver == null)
            {
                throw new KeyNotFoundException("Driver not found.");
            }

            driver.Status = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateDriverAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("Driver not found.");
            }
            else
            {
                user.Status = false;
                await _context.SaveChangesAsync();
            }
        }
    }

}
