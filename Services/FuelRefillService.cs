using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class FuelRefillService : IFuelRefillService
    {
        private readonly FleetPulseDbContext _context;

        public FuelRefillService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<List<FuelRefillDTO>> GetAllFuelRefillsAsync()
        {
            return await _context.FuelRefills
                .Include(fr => fr.Vehicle)
                .Include(fr => fr.User)
                .Select(fr => new FuelRefillDTO
                {
                    FuelRefillId = fr.FuelRefillId,
                    Date = fr.Date,
                    Time = fr.Time,
                    LiterCount = fr.LiterCount,
                    FType = fr.FType,
                    Cost = fr.Cost,
                    VehicleRegistrationNo = fr.Vehicle.VehicleRegistrationNo,
                    NIC = fr.User.NIC,
                    Status = fr.Status
                })
                .ToListAsync();
        }

        public async Task<FuelRefill> GetFuelRefillByIdAsync(int fuelRefillId)
        {
            return await _context.FuelRefills
                .Include(fr => fr.Vehicle)
                .Include(fr => fr.User)
                .FirstOrDefaultAsync(fr => fr.FuelRefillId == fuelRefillId);
        }

        public bool DoesFuelRefillExist(string fType)
        {
            return _context.FuelRefills.Any(fr => fr.FType == fType);
        }

        public async Task<FuelRefill?> AddFuelRefillAsync(FuelRefillDTO fuelRefillDto)
        {
            var user = await _context.Users.FindAsync(fuelRefillDto.UserId);
            if (user == null)
            {
                Console.WriteLine($"User with ID {fuelRefillDto.UserId} not found.");
                return null;
            }

            var vehicle = await _context.Vehicles.FindAsync(fuelRefillDto.VehicleId);
            if (vehicle == null)
            {
                Console.WriteLine($"Vehicle with ID {fuelRefillDto.VehicleId} not found.");
                return null;
            }

            var fuelRefill = new FuelRefill
            {
                Date = fuelRefillDto.Date,
                Time = fuelRefillDto.Time,
                LiterCount = fuelRefillDto.LiterCount,
                FType = fuelRefillDto.FType,
                Cost = fuelRefillDto.Cost,
                Status = fuelRefillDto.Status,
                UserId = fuelRefillDto.UserId,
                VehicleId = fuelRefillDto.VehicleId,
            };

            _context.FuelRefills.Add(fuelRefill);
            await _context.SaveChangesAsync();
            return fuelRefill;
        }
        
        public async Task<FuelRefill> UpdateFuelRefillAsync(int id, FuelRefillDTO fuelRefillDto)
        {
            var fuelRefill = await _context.FuelRefills.FindAsync(id);
            if (fuelRefill == null)
            {
                return null;
            }

            fuelRefill.Date = fuelRefillDto.Date;
            fuelRefill.Time = fuelRefillDto.Time;
            fuelRefill.LiterCount = fuelRefillDto.LiterCount;
            fuelRefill.FType = fuelRefillDto.FType;
            fuelRefill.Cost = fuelRefillDto.Cost;
            fuelRefill.Status = fuelRefillDto.Status;

            if (fuelRefillDto.UserId != 0)
            {
                fuelRefill.UserId = fuelRefillDto.UserId;
            }

            if (fuelRefillDto.VehicleId != 0)
            {
                fuelRefill.VehicleId = fuelRefillDto.VehicleId;
            }

            await _context.SaveChangesAsync();
            return fuelRefill;
        }
        
        public async Task<bool> ActivateFuelRefillAsync(int fuelRefillId)
        {
            var fuelRefill = await _context.FuelRefills.FindAsync(fuelRefillId);
            if (fuelRefill == null) return false;

            fuelRefill.Status = true;
            _context.FuelRefills.Update(fuelRefill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateFuelRefillAsync(int fuelRefillId)
        {
            var fuelRefill = await _context.FuelRefills.FindAsync(fuelRefillId);
            if (fuelRefill == null) return false;

            fuelRefill.Status = false;
            _context.FuelRefills.Update(fuelRefill);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> IsFuelRefillExist(int fuelRefillId)
        {
            return await _context.FuelRefills.AnyAsync(fr => fr.FuelRefillId == fuelRefillId);
        }

        private bool FuelRefillIsAssociatedWithUser(FuelRefill fuelRefill)
        {
            return _context.Users.Any(u => u.FuelRefills.Any(fr => fr.FuelRefillId == fuelRefill.FuelRefillId));
        }
    }
}
