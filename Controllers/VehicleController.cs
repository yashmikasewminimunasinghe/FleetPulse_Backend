using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsyncDisplay();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicleById(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicleAsync([FromBody] VehicleDTO vehicleDto)
        {
            var response = new ApiResponse();
            try
            {
                var vehicleExists = _vehicleService.DoesVehicleExists(vehicleDto.VehicleRegistrationNo);
                if (vehicleExists)
                {
                    response.Message = "Vehicle already exists";
                    return new JsonResult(response);
                }

                var vehicle = new Vehicle
                {
                    VehicleRegistrationNo = vehicleDto.VehicleRegistrationNo,
                    LicenseNo = vehicleDto.LicenseNo,
                    LicenseExpireDate = vehicleDto.LicenseExpireDate,
                    VehicleColor = vehicleDto.VehicleColor,
                    Status = vehicleDto.Status,
                    VehicleTypeId = vehicleDto.VehicleTypeId,
                    ManufactureId = vehicleDto.ManufactureId,
                    AccidentId = vehicleDto.AccidentId,
                    TripId = vehicleDto.TripId,
                    VehicleMaintenances = vehicleDto.VehicleMaintenanceIds?.Select(id => new VehicleMaintenance {MaintenanceId = id }).ToList(),
                    FuelRefills = vehicleDto.FuelRefillIds?.Select(id => new FuelRefill {FuelRefillId = id }).ToList()
                };

                var addedVehicle = await _vehicleService.AddVehicleAsync(vehicle);

                if (addedVehicle != null)
                {
                    response.Status = true;
                    response.Message = "Added Successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Vehicle";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return new JsonResult(response);
        }

        [HttpPut("UpdateVehicle")]
        public async Task<IActionResult> UpdateVehicle([FromBody] VehicleDTO vehicleDto)
        {
            try
            {
                var existingVehicle = await _vehicleService.IsVehicleExist(vehicleDto.VehicleId);

                if (!existingVehicle)
                {
                    return NotFound("Vehicle with Id not found");
                }

                var vehicle = new Vehicle
                {
                    VehicleId = vehicleDto.VehicleId,
                    VehicleRegistrationNo = vehicleDto.VehicleRegistrationNo,
                    LicenseNo = vehicleDto.LicenseNo,
                    LicenseExpireDate = vehicleDto.LicenseExpireDate,
                    VehicleColor = vehicleDto.VehicleColor,
                    Status = vehicleDto.Status,
                    VehicleTypeId = vehicleDto.VehicleTypeId,
                    ManufactureId = vehicleDto.ManufactureId,
                    AccidentId = vehicleDto.AccidentId,
                    TripId = vehicleDto.TripId,
                    VehicleMaintenances = vehicleDto.VehicleMaintenanceIds?.Select(id => new VehicleMaintenance { MaintenanceId = id }).ToList(),
                    FuelRefills = vehicleDto.FuelRefillIds?.Select(id => new FuelRefill { FuelRefillId = id }).ToList()
                };

                var result = await _vehicleService.UpdateVehicleAsync(vehicle);
                if (result)
                {
                    return Ok("Vehicle updated successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to update Vehicle.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the vehicle: {ex.Message}");
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateVehicle(int id)
        {
            try
            {
                await _vehicleService.DeactivateVehicleAsync(id);
                return Ok("Vehicle deactivated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateVehicle(int id)
        {
            try
            {
                await _vehicleService.ActivateVehicleAsync(id);
                return Ok("Vehicle activated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
