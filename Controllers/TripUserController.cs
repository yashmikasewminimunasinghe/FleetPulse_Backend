using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripUserController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;

        public TripUserController(FleetPulseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<TripUser>> Get()
        {
            return await _context.TripUsers.ToListAsync();
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> Get(int userid)
        {
            if (userid < 1)
                return BadRequest();

            var tripUser = await _context.TripUsers.FirstOrDefaultAsync(m => m.UserId == userid);
            if (tripUser == null)
                return NotFound();
            return Ok(tripUser);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TripUser tripUser)
        {
            _context.TripUsers.Add(tripUser);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(TripUser tripUserData)
        {
            if (tripUserData == null || tripUserData.UserId == 0)
                return BadRequest();

            var tripUser = await _context.TripUsers.FindAsync(tripUserData.UserId);
            if (tripUser == null)
                return NotFound();

            tripUser.UserId = tripUserData.UserId;
            tripUser.User = tripUserData.User;
            tripUser.TripId = tripUserData.TripId;
            tripUser.Trip = tripUserData.Trip;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{userid}")]
        public async Task<IActionResult> Delete(int userid)
        {
            if (userid < 1)
                return BadRequest();
            var tripUser = await _context.TripUsers.FindAsync(userid);
            if (tripUser == null)
                return NotFound();
            _context.TripUsers.Remove(tripUser);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
