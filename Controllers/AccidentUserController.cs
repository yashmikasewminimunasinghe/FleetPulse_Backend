using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentUserController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;

        public AccidentUserController(FleetPulseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<AccidentUser>> Get()
        {
            return await _context.AccidentUsers.ToListAsync();
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> Get(int userid)
        {
            if (userid < 1)
                return BadRequest();

            var accidentUser = await _context.AccidentUsers.FirstOrDefaultAsync(m => m.UserId == userid);
            if (accidentUser == null)
                return NotFound();
            return Ok(accidentUser);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AccidentUser accidentUser)
        {
            if (accidentUser == null)
                return BadRequest();

            _context.Add(accidentUser);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(AccidentUser accidentUserData)
        {
            if (accidentUserData == null || accidentUserData.UserId == 0)
                return BadRequest();

            var accidentUser = await _context.AccidentUsers.FindAsync(accidentUserData.UserId);
            if (accidentUser == null)
                return NotFound();

            accidentUser.UserId = accidentUserData.UserId;
            accidentUser.User = accidentUserData.User;
            accidentUser.AccidentId = accidentUserData.AccidentId;
            accidentUser.Accident = accidentUserData.Accident;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{userid}")]
        public async Task<IActionResult> Delete(int userid)
        {
            if (userid < 1)
                return BadRequest();

            var accidentUser = await _context.AccidentUsers.FindAsync(userid);
            if (accidentUser == null)
                return NotFound();

            _context.AccidentUsers.Remove(accidentUser);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

