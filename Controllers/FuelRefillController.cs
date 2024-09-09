using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelRefillController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IFuelRefillService _fuelRefillService;

        public FuelRefillController(IAuthService authService, IFuelRefillService fuelRefillService)
        {
            _authService = authService;
            _fuelRefillService = fuelRefillService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FuelRefillDTO>>> GetAllFuelRefills()
        {
            var fuelRefills = await _fuelRefillService.GetAllFuelRefillsAsync();
            return Ok(fuelRefills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuelRefill>> GetFuelRefillById(int id)
        {
            var fuelRefill = await _fuelRefillService.GetFuelRefillByIdAsync(id);
            if (fuelRefill == null)
            {
                return NotFound();
            }
            return Ok(fuelRefill);
        }
        
        [HttpPost]
        public async Task<ActionResult<FuelRefill>> AddFuelRefill([FromBody] FuelRefillDTO fuelRefillDto)
        {
            if (fuelRefillDto.UserId == 0)
            {
                var userId = await _authService.GetUserIdByNICAsync(fuelRefillDto.NIC);
                if (!userId.HasValue)
                {
                    return BadRequest("User not found");
                }

                fuelRefillDto.UserId = userId.Value;
            }

            var addedFuelRefill = await _fuelRefillService.AddFuelRefillAsync(fuelRefillDto);
            if (addedFuelRefill == null)
            {
                return BadRequest("User or Vehicle not found");
            }

            return CreatedAtAction(nameof(GetFuelRefillById), new { id = addedFuelRefill.FuelRefillId }, addedFuelRefill);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFuelRefill(int id, [FromBody] FuelRefillDTO fuelRefillDto)
        {
            var updatedFuelRefill = await _fuelRefillService.UpdateFuelRefillAsync(id, fuelRefillDto);
            if (updatedFuelRefill == null)
            {
                return NotFound("Fuel refill not found.");
            }
            return Ok(updatedFuelRefill);
        }
        
        [HttpPost("activate/{id}")]
        public async Task<IActionResult> ActivateFuelRefill(int id)
        {
            var result = await _fuelRefillService.ActivateFuelRefillAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpPost("deactivate/{id}")]
        public async Task<IActionResult> DeactivateFuelRefill(int id)
        {
            var result = await _fuelRefillService.DeactivateFuelRefillAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
