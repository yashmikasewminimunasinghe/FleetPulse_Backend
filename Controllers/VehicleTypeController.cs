using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypeController(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetAllVehicleTypes()
        {
            var vehicleTypes = await _vehicleTypeService.GetAllVehicleTypesAsync();
            return Ok(vehicleTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleType>> GetVehicleTypeById(int id)
        {
            var vehicleType = await _vehicleTypeService.GetVehicleTypeByIdAsync(id);
            if (vehicleType == null)
                return NotFound();

            return Ok(vehicleType);
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicleTypeAsync([FromBody] VehicleTypeDTO Type)
        {
            var response = new ApiResponse();
            try
            {
                var vehicleType = new VehicleType
                {
                    Type = Type.Type,
                    Status = Type.Status
                };

                var vehicleTypeExists = _vehicleTypeService.DoesVehicleTypeExists(vehicleType.Type);
                if (vehicleTypeExists)
                {
                    response.Message = "Vehicle Type already exists";
                    return new JsonResult(response);
                }

                var addedVehicleType = await _vehicleTypeService.AddVehicleTypeAsync(vehicleType);

                if (addedVehicleType != null)
                {
                    response.Status = true;
                    response.Message = "Added Successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Vehicle Type";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return new JsonResult(response);
        }
        [HttpPut("UpdateVehicleType")]
        public async Task<IActionResult> UpdateVehicleType([FromBody] VehicleTypeDTO vehicleTypeDto)
        {
            try
            {
                var existingVehicleType = await _vehicleTypeService.IsVehicleTypeExist(vehicleTypeDto.VehicleTypeId);

                if (!existingVehicleType)
                {
                    return NotFound("Vehicle Type with Id not found");
                }

                var vehicleType = new VehicleType
                {
                    VehicleTypeId = vehicleTypeDto.VehicleTypeId,
                    Type = vehicleTypeDto.Type,
                    Status = vehicleTypeDto.Status
                };

                var result = await _vehicleTypeService.UpdateVehicleTypeAsync(vehicleType);
                if (result)
                {
                    return Ok("Vehicle Type updated successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to update Vehicle Type.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the vehicle type: {ex.Message}");
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateVehicleType(int id)
        {
            try
            {
                await _vehicleTypeService.DeactivateVehicleTypeAsync(id);
                return Ok("Vehicle Type deactivated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateVehicleType(int id)
        {
            try
            {
                await _vehicleTypeService.ActivateVehicleTypeAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
