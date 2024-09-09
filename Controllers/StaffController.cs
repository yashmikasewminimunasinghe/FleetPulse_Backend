using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.DTOs;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services;
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
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAllStaffs()
        {
            try
            {
                var staffs = await _staffService.GetAllStaffsAsync();
                var staffDTOs = new List<StaffDTO>();
                foreach (var staff in staffs)
                {
                    staffDTOs.Add(MapUserToDTO(staff)); // Assuming a mapping method exists
                }
                return Ok(staffDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving staff: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDTO>> GetStaffById(int id)
        {
            try
            {
                var staff = await _staffService.GetStaffByIdAsync(id);
                if (staff == null)
                    return NotFound();

                var staffDTO = MapUserToDTO(staff); // Assuming a mapping method exists
                return Ok(staffDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the staff: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddStaffAsync([FromBody] StaffDTO staffDto)
        {
            var response = new ApiResponse();
            try
            {
                var user = new User // Use User entity instead of Driver if it represents your driver entity
                {
                    FirstName = staffDto.FirstName,
                    LastName = staffDto.LastName,
                    DateOfBirth = staffDto.DateOfBirth,
                    NIC = staffDto.NIC,
                    EmailAddress = staffDto.EmailAddress,
                    PhoneNo = staffDto.PhoneNo,
                    EmergencyContact = staffDto.EmergencyContact,
                    JobTitle = staffDto.JobTitle,
                    Status = staffDto.Status,
                    HashedPassword = BC.HashPassword(staffDto.Password),
                    UserName = staffDto.UserName,
                };

                var staffExists = await _staffService.IsStaffExist(user.UserId); // Assuming UserId exists on User entity
                if (staffExists)
                {
                    response.Message = "Staff already exists";
                    return new JsonResult(response);
                }

                var addedStaff = await _staffService.AddStaffAsync(user); // Assuming AddDriverAsync method expects User entity

                if (addedStaff != null)
                {
                    response.Status = true;
                    response.Message = "Staff added successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Staff";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return new JsonResult(response);
        }

        [HttpPut("UpdateStaff")]
        public async Task<IActionResult> UpdateStaff([FromBody] StaffDTO staffDto)
        {
            try
            {
                var existingStaff = await _staffService.IsStaffExist(staffDto.UserId); 

                if (!existingStaff)
                {
                    return NotFound("Staff with Id not found");
                }

                User staff = await _staffService.GetStaffByIdAsync(staffDto.UserId);

                {
                  staff.UserId = staffDto.UserId; 
                  staff.FirstName = staffDto.FirstName;
                  staff.LastName = staffDto.LastName;
                  staff.DateOfBirth = staffDto.DateOfBirth;
                  staff.NIC = staffDto.NIC; 
                  staff.EmailAddress = staffDto.EmailAddress;
                  staff.PhoneNo = staffDto.PhoneNo;
                  staff.EmergencyContact = staffDto.EmergencyContact;
                  staff.JobTitle = staffDto.JobTitle;
                  staff.Status = staffDto.Status;
                };

                var result = await _staffService.UpdateStaffAsync(staff); // Assuming UpdateDriverAsync method expects User entity
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the staff: {ex.Message}");
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateStaff(int id)
        {
            try
            {
                await _staffService.DeactivateStaffAsync(id);
                return Ok("Staff deactivated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateStaff(int id)
        {
            try
            {
                await _staffService.ActivateStaffAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        private StaffDTO MapUserToDTO(User user)
        {
            return new StaffDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                NIC = user.NIC,           
                EmailAddress = user.EmailAddress,
                PhoneNo = user.PhoneNo,
                EmergencyContact = user.EmergencyContact,
                JobTitle = user.JobTitle,
                Status = user.Status,
                UserName = user.UserName,
            };
        }
    }
}
