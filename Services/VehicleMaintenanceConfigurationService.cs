using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleMaintenanceConfigurationService : IVehicleMaintenanceConfigurationService
    {
        private readonly FleetPulseDbContext _context;
        private readonly ILogger<VehicleMaintenanceConfigurationService> _logger;

        public VehicleMaintenanceConfigurationService(FleetPulseDbContext context, ILogger<VehicleMaintenanceConfigurationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<VehicleMaintenanceConfigurationDTO> AddVehicleMaintenanceConfigurationAsync(VehicleMaintenanceConfigurationDTO dto)
        {
            var entity = new VehicleMaintenanceConfiguration
            {
                VehicleId = dto.VehicleId,
                VehicleRegistrationNo = dto.VehicleRegistrationNo,
                VehicleMaintenanceTypeId = dto.VehicleMaintenanceTypeId,
                TypeName = dto.TypeName,
                Duration = dto.Duration,
                Status = dto.Status
            };

            _context.VehicleMaintenanceConfigurations.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<VehicleMaintenanceConfigurationDTO> GetVehicleMaintenanceConfigurationByIdAsync(int id)
        {
            var entity = await _context.VehicleMaintenanceConfigurations.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            return new VehicleMaintenanceConfigurationDTO
            {
                Id = entity.Id,
                VehicleId = entity.VehicleId,
                VehicleRegistrationNo = entity.VehicleRegistrationNo,
                VehicleMaintenanceTypeId = entity.VehicleMaintenanceTypeId,
                TypeName = entity.TypeName,
                Duration = entity.Duration,
                Status = entity.Status
            };
        }

        public async Task<IEnumerable<VehicleMaintenanceConfigurationDTO>> GetAllVehicleMaintenanceConfigurationsAsync()
        {
            return await _context.VehicleMaintenanceConfigurations
                .Select(entity => new VehicleMaintenanceConfigurationDTO
                {
                    Id = entity.Id,
                    VehicleId = entity.VehicleId,
                    VehicleRegistrationNo = entity.VehicleRegistrationNo,
                    VehicleMaintenanceTypeId = entity.VehicleMaintenanceTypeId,
                    TypeName = entity.TypeName,
                    Duration = entity.Duration,
                    Status = entity.Status
                }).ToListAsync();
        }

        public async Task<bool> UpdateVehicleMaintenanceConfigurationAsync(VehicleMaintenanceConfigurationDTO dto)
        {
            var entity = await _context.VehicleMaintenanceConfigurations.FindAsync(dto.Id);
            if (entity == null)
            {
                return false;
            }

            entity.VehicleId = dto.VehicleId;
            entity.VehicleRegistrationNo = dto.VehicleRegistrationNo;
            entity.VehicleMaintenanceTypeId = dto.VehicleMaintenanceTypeId;
            entity.TypeName = dto.TypeName;
            entity.Duration = dto.Duration;
            entity.Status = dto.Status;

            _context.VehicleMaintenanceConfigurations.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteVehicleMaintenanceConfigurationAsync(int id)
        {
            var entity = await _context.VehicleMaintenanceConfigurations.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.VehicleMaintenanceConfigurations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    
        public async Task<List<VehicleMaintenanceConfigurationDTO>> GetDueMaintenanceTasksAsync()
        {
            return await _context.VehicleMaintenanceConfigurations
                .Select(entity => new VehicleMaintenanceConfigurationDTO
                {
                    Id = entity.Id,
                    VehicleId = entity.VehicleId,
                    VehicleRegistrationNo = entity.VehicleRegistrationNo,
                    VehicleMaintenanceTypeId = entity.VehicleMaintenanceTypeId,
                    TypeName = entity.TypeName,
                    Duration = entity.Duration,
                    Status = entity.Status
                }).ToListAsync();
        }

        private DateTime CalculateNextMaintenanceDate(DateTime lastMaintenanceDate, string duration)
        {
            try
            {
                var durationParts = duration.Split(' ');

                if (durationParts.Length != 2)
                {
                    throw new FormatException("Invalid duration format. Expected '<value> <unit>' (e.g., '30 days').");
                }

                var durationValue = int.Parse(durationParts[0]);
                var durationType = durationParts[1].Trim().ToLower(); // Convert to lowercase for case insensitivity

                switch (durationType)
                {
                    case "days":
                        return lastMaintenanceDate.AddDays(durationValue);
                    case "months":
                        return lastMaintenanceDate.AddMonths(durationValue);
                    case "years":
                        return lastMaintenanceDate.AddYears(durationValue);
                    case "seconds":
                        return lastMaintenanceDate.AddSeconds(durationValue);
                    default:
                        throw new FormatException(
                            $"Unsupported duration type: {durationType}. Supported types: days, months, years, seconds.");
                }
            }
            catch (FormatException ex)
            {
                // Log the exception with detailed information
                _logger.LogError(ex, $"Invalid duration format or type: {duration}");
                throw; // Rethrow the exception to propagate it up
            }
            catch (Exception ex)
            {
                // Log any unexpected exceptions
                _logger.LogError(ex, $"Error processing duration: {duration}");
                throw; // Rethrow the exception to propagate it up
            }
        }
    }
}
