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
    public class HelperController : ControllerBase
    {
        private readonly IHelperService _helperService;

        public HelperController(IHelperService helperService)
        {
            _helperService = helperService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HelperDTO>>> GetAllHelpers()
        {
            try
            {
                var helpers = await _helperService.GetAllHelpersAsync();
                var helperDTOs = new List<HelperDTO>();
                foreach (var helper in helpers)
                {
                    helperDTOs.Add(MapUserToDTO(helper)); // Assuming a mapping method exists
                }
                return Ok(helperDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving helpers: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HelperDTO>> GetHelperById(int id)
        {
            try
            {
                var helper = await _helperService.GetHelperByIdAsync(id);
                if (helper == null)
                    return NotFound();

                var helperDTO = MapUserToDTO(helper); // Assuming a mapping method exists
                return Ok(helperDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the helper: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddHelperAsync([FromBody] HelperDTO helperDto)
        {
            var response = new ApiResponse();
            try
            {
                var user = new User // Use User entity instead of Driver if it represents your driver entity
                {
                    FirstName = helperDto.FirstName,
                    LastName = helperDto.LastName,
                    DateOfBirth = helperDto.DateOfBirth,
                    NIC = helperDto.NIC,                  
                    EmailAddress = helperDto.EmailAddress,
                    PhoneNo = helperDto.PhoneNo,
                    EmergencyContact = helperDto.EmergencyContact,
                    BloodGroup = helperDto.BloodGroup,
                    Status = helperDto.Status,
                    JobTitle = "Helper",
                    HashedPassword = BC.HashPassword(helperDto.Password),
                    UserName = helperDto.UserName,
                };

                var helperExists = await _helperService.IsHelperExist(user.UserId); 
                if (helperExists)
                {
                    response.Message = "Helper already exists";
                    return new JsonResult(response);
                }

                var addHelper = await _helperService.AddHelperAsync(user); 

                if (addHelper != null)
                {
                    response.Status = true;
                    response.Message = "Helper added successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Helper";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return new JsonResult(response);
        }

        [HttpPut("UpdateHelper")]
        public async Task<IActionResult> UpdateHelper([FromBody] HelperDTO helperDto)
        {
            try
            {
                var existingHelper = await _helperService.IsHelperExist(helperDto.UserId); // Assuming UserId exists on DriverDTO

                if (!existingHelper)
                {
                    return NotFound("Helper with Id not found");
                }

                User helper = await _helperService.GetHelperByIdAsync(helperDto.UserId);

                helper.UserId = helperDto.UserId;
                helper.FirstName = helperDto.FirstName;
                helper.LastName = helperDto.LastName;
                helper.DateOfBirth = helperDto.DateOfBirth;
                helper.NIC = helperDto.NIC;
                helper.EmailAddress = helperDto.EmailAddress;
                helper.PhoneNo = helperDto.PhoneNo;
                helper.EmergencyContact = helperDto.EmergencyContact;
                helper.BloodGroup = helperDto.BloodGroup;
                helper.Status = helperDto.Status;

                var result = await _helperService.UpdateHelperAsync(helper); // Assuming UpdateDriverAsync method expects User entity
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the helper: {ex.Message}");
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateHelper(int id)
        {
            try
            {
                await _helperService.DeactivateHelperAsync(id);
                return Ok("Helper deactivated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateHelper(int id)
        {
            try
            {
                await _helperService.ActivateHelperAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        private HelperDTO MapUserToDTO(User user)
        {
            return new HelperDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                NIC = user.NIC,
                EmailAddress = user.EmailAddress,
                PhoneNo = user.PhoneNo,
                EmergencyContact = user.EmergencyContact,
                BloodGroup = user.BloodGroup,
                Status = user.Status,
                UserName = user.UserName,
            };
        }
    }
}
