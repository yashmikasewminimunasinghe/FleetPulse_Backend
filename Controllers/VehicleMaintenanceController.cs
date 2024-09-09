using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMaintenanceController : ControllerBase
    {
        private readonly IVehicleMaintenanceService _maintenanceService;

        public VehicleMaintenanceController(IVehicleMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVehicleMaintenancesAsync()
        {
            var maintenances = await _maintenanceService.GetAllVehicleMaintenancesAsync();
            return Ok(maintenances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetVehicleMaintenanceByIdAsync(int id)
        {
            var maintenance = await _maintenanceService.GetVehicleMaintenanceByIdAsync(id);
            if (maintenance == null)
                return NotFound();

            return Ok(maintenance);
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicleMaintenanceAsync([FromBody] VehicleMaintenanceDTO vehicleMaintenance)
        {
            var response = new ApiResponse();
            try
            {
                var maintenance = new VehicleMaintenance
                {
                    MaintenanceDate = vehicleMaintenance.MaintenanceDate,
                    PartsReplaced = vehicleMaintenance.PartsReplaced,
                    ServiceProvider = vehicleMaintenance.ServiceProvider,
                    Cost = vehicleMaintenance.Cost,
                    Status = vehicleMaintenance.Status,
                    SpecialNotes = vehicleMaintenance.SpecialNotes,
                    VehicleId = vehicleMaintenance.VehicleId,
                    VehicleMaintenanceTypeId = vehicleMaintenance.VehicleMaintenanceTypeId,
                };

                var addedMaintenance = await _maintenanceService.AddVehicleMaintenanceAsync(maintenance);

                response.Status = true;
                response.Message = "Added Successfully";
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
                return new JsonResult(response);
            }
        }

        [HttpPut("{id}")] // Add this method
        public async Task<IActionResult> UpdateVehicleMaintenanceAsync(int id,
            [FromBody] VehicleMaintenanceDTO vehicleMaintenance)
        {
            try
            {
                var maintenance = new VehicleMaintenance
                {
                    MaintenanceDate = vehicleMaintenance.MaintenanceDate,
                    PartsReplaced = vehicleMaintenance.PartsReplaced,
                    ServiceProvider = vehicleMaintenance.ServiceProvider,
                    Cost = vehicleMaintenance.Cost,
                    Status = vehicleMaintenance.Status,
                    SpecialNotes = vehicleMaintenance.SpecialNotes,
                    VehicleId = vehicleMaintenance.VehicleId,
                    VehicleMaintenanceTypeId = vehicleMaintenance.VehicleMaintenanceTypeId,
                };

                var updatedMaintenance = await _maintenanceService.UpdateVehicleMaintenanceAsync(id, maintenance);

                if (updatedMaintenance == null)
                {
                    return NotFound();
                }

                return Ok(updatedMaintenance);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateMaintenance(int id)
        {
            try
            {
                await _maintenanceService.DeactivateMaintenanceAsync(id);
                return Ok("Maintenance deactivated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
