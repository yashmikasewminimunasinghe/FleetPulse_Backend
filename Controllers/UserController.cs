using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;
        private int UserId;

        public UserController(FleetPulseDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }
        [HttpGet("{UserId}")]
        public async Task<IActionResult> Get(int UserId)
        {
            if (UserId < 1)
                return BadRequest();


            var User = await _context.Users.FirstOrDefaultAsync(m => m.UserId == UserId);
            if (User == null)
                return NotFound();
            return Ok(User);
        }
        [HttpPost]
        public async Task<IActionResult> Post(User User)
        {
            _context.Add(User);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(User UserData)
        {
            if (UserData == null || UserData.UserId == 0)
                return BadRequest();

            var User = await _context.Users.FindAsync(UserData.UserId);
            if (User == null)
                return NotFound();
            User.UserId = UserData.UserId;
            User.FirstName = UserData.FirstName;
            User.LastName = UserData.LastName;
            User.NIC = UserData.NIC;
            User.DriverLicenseNo = UserData.DriverLicenseNo;
            User.LicenseExpiryDate = UserData.LicenseExpiryDate;
            User.BloodGroup = UserData.BloodGroup;
            User.DateOfBirth = UserData.DateOfBirth;
            User.PhoneNo = UserData.PhoneNo;
            User.UserName = UserData.UserName;
            User.HashedPassword = UserData.HashedPassword;
            User.EmailAddress = UserData.EmailAddress;
            User.EmergencyContact = UserData.EmergencyContact;
            User.JobTitle = UserData.JobTitle;
            User.ProfilePicture = UserData.ProfilePicture;
            User.Status = UserData.Status;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{userid}")]
        public async Task<IActionResult> Delete(int userid)
        {
            if (UserId < 1)
                return BadRequest();
            var Trip = await _context.Users.FindAsync(userid);
            if (Trip == null)
                return NotFound();
            _context.Users.Remove(Trip);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}


