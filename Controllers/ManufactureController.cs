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
    public class ManufactureController : ControllerBase
    {
        private readonly IManufactureService _manufactureService;

        public ManufactureController(IManufactureService manufactureService)
        {
            _manufactureService = manufactureService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacture>>> GetAllManufactures()
        {
            var manufactures = await _manufactureService.GetAllManufacturesAsync();
            return Ok(manufactures);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacture>> GetManufactureById(int id)
        {
            var manufacture = await _manufactureService.GetManufactureByIdAsync(id);
            if (manufacture == null)
                return NotFound();

            return Ok(manufacture);
        }

        [HttpPost]
        public async Task<ActionResult> AddManufactureAsync([FromBody] ManufactureDTO manufactureDto)
        {
            var response = new ApiResponse();
            try
            {
                var manufacture = new Manufacture
                {
                    Manufacturer = manufactureDto.Manufacturer,
                    Status = manufactureDto.Status
                };

                var manufactureExists = _manufactureService.DoesManufactureExists(manufacture.Manufacturer);
                if (manufactureExists)
                {
                    response.Message = "Manufacturer already exists";
                    return new JsonResult(response);
                }

                var addedManufacture = await _manufactureService.AddManufactureAsync(manufacture);

                if (addedManufacture != null)
                {
                    response.Status = true;
                    response.Message = "Added Successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Manufacturer";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return new JsonResult(response);
        }

        [HttpPut("UpdateManufacture")]
        public async Task<IActionResult> UpdateManufacture([FromBody] ManufactureDTO manufactureDto)
        {
            try
            {
                var existingManufacture = await _manufactureService.IsManufactureExist(manufactureDto.ManufactureId);

                if (!existingManufacture)
                {
                    return NotFound("Manufacturer with Id not found");
                }

                var manufacture = new Manufacture
                {
                    ManufactureId = manufactureDto.ManufactureId,
                    Manufacturer = manufactureDto.Manufacturer,
                    Status = manufactureDto.Status
                };
                var result = await _manufactureService.UpdateManufactureAsync(manufacture);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the manufacturer: {ex.Message}");
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateManufacture(int id)
        {
            try
            {
                await _manufactureService.DeactivateManufactureAsync(id);
                return Ok("Manufacturer deactivated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateManufacture(int id)
        {
            try
            {
                await _manufactureService.ActivateManufactureAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
