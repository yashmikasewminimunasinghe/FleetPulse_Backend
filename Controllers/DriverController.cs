using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.DTOs;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDTO>>> GetAllDrivers()
        {
            try
            {
                var drivers = await _driverService.GetAllDriversAsync();
                var driverDTOs = new List<DriverDTO>();
                foreach (var driver in drivers)
                {
                    driverDTOs.Add(MapUserToDTO(driver)); // Assuming a mapping method exists
                }
                return Ok(driverDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving drivers: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDTO>> GetDriverById(int id)
        {
            try
            {
                var driver = await _driverService.GetDriverByIdAsync(id);
                if (driver == null)
                    return NotFound();

                var driverDTO = MapUserToDTO(driver); // Assuming a mapping method exists
                return Ok(driverDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the driver: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddDriverAsync([FromBody] DriverDTO driverDto)
        {
            var response = new ApiResponse();
            try
            {
                var user = new User // Use User entity instead of Driver if it represents your driver entity
                {
                    FirstName = driverDto.FirstName,
                    LastName = driverDto.LastName,
                    DateOfBirth = driverDto.DateOfBirth,
                    NIC = driverDto.NIC,
                    DriverLicenseNo = driverDto.DriverLicenseNo,
                    LicenseExpiryDate = driverDto.LicenseExpiryDate,
                    EmailAddress = driverDto.EmailAddress,
                    PhoneNo = driverDto.PhoneNo,
                    EmergencyContact = driverDto.EmergencyContact,
                    BloodGroup = driverDto.BloodGroup,
                    Status = driverDto.Status,
                    JobTitle = "Driver",
                    HashedPassword = BC.HashPassword(driverDto.Password),
                    UserName = driverDto.UserName,
                };

                var driverExists = await _driverService.IsDriverExist(user.UserId); // Assuming UserId exists on User entity
                if (driverExists)
                {
                    response.Message = "Driver already exists";
                    return new JsonResult(response);
                }

                var addedDriver = await _driverService.AddDriverAsync(user); // Assuming AddDriverAsync method expects User entity

                if (addedDriver != null)
                {
                    response.Status = true;
                    response.Message = "Driver added successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Driver";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return new JsonResult(response);
        }

        [HttpPut("UpdateDriver")]
        public async Task<IActionResult> UpdateDriver([FromBody] DriverDTO driverDto)
        {
            try
            {
                var existingDriver = await _driverService.IsDriverExist(driverDto.UserId); // Assuming UserId exists on DriverDTO

                if (!existingDriver)
                {
                    return NotFound("Driver with Id not found");
                }

                User driver = await _driverService.GetDriverByIdAsync(driverDto.UserId);

                driver.UserId = driverDto.UserId; // Assuming UserId exists on User entity
                driver.FirstName = driverDto.FirstName;
                driver.LastName = driverDto.LastName;
                driver.DateOfBirth = driverDto.DateOfBirth;
                driver.DriverLicenseNo = driverDto.DriverLicenseNo;
                driver.LicenseExpiryDate = driverDto.LicenseExpiryDate;
                driver.EmailAddress = driverDto.EmailAddress;
                driver.PhoneNo = driverDto.PhoneNo;
                driver.EmergencyContact = driverDto.EmergencyContact;
                driver.BloodGroup = driverDto.BloodGroup;
                driver.Status = driverDto.Status;

                var result = await _driverService.UpdateDriverAsync(driver); // Assuming UpdateDriverAsync method expects User entity
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the driver: {ex.Message}");
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateDriver(int id)
        {
            try
            {
                await _driverService.DeactivateDriverAsync(id);
                return Ok("Driver deactivated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateDriver(int id)
        {
            try
            {
                await _driverService.ActivateDriverAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

   
        private DriverDTO MapUserToDTO(User user)
        {
            return new DriverDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                NIC = user.NIC,
                DriverLicenseNo = user.DriverLicenseNo,
                LicenseExpiryDate = user.LicenseExpiryDate,
                EmailAddress = user.EmailAddress,
                PhoneNo = user.PhoneNo,
                EmergencyContact = user.EmergencyContact,
                BloodGroup = user.BloodGroup,
                Status = user.Status,
                UserName = user.UserName
            };
        }
    }
}
