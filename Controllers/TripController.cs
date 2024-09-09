using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAllTrips()
        {
            var trips = await _tripService.GetAllTripsAsync();
            return Ok(trips);
        }

        [HttpGet("{tripId}")]
        public async Task<ActionResult<Trip>> GetTripById(int tripId)
        {
            var trip = await _tripService.GetTripByIdAsync(tripId);
            if (trip == null)
                return NotFound();

            return Ok(trip);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddTripAsync([FromBody] TripDTO tripDTO)
        {
            var response = new ApiResponse();
            try
            {
                var trip = new Trip
                {
                    TripId = tripDTO.TripId,
                    Date = tripDTO.Date,
                    StartTime = tripDTO.StartTime,
                    EndTime = tripDTO.EndTime,
                    StartMeterValue = tripDTO.StartMeterValue,
                    EndMeterValue = tripDTO.EndMeterValue,
                    Status = tripDTO.Status
                };

                var tripExists = _tripService.DoesTripExists(trip.TripId);
                if (tripExists)
                {
                    response.Message = "Trip already exists";
                    return Conflict(response);
                }

                var addedTrip = await _tripService.AddTripAsync(trip);

                if (addedTrip != null)
                {
                    response.Status = true;
                    response.Message = "Added Successfully";
                    return Ok(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Trip";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrip(int id, [FromBody] TripDTO tripDTO)
        {
            try
            {
                var existingTrip = await _tripService.IsTripExist(id);

                if (!existingTrip)
                {
                    return NotFound("Trip with Id not found");
                }

                var trip = new Trip
                {
                    TripId = tripDTO.TripId,
                    Date = tripDTO.Date,
                    StartTime = tripDTO.StartTime,
                    EndTime = tripDTO.EndTime,
                    
                    StartMeterValue = tripDTO.StartMeterValue,
                    EndMeterValue = tripDTO.EndMeterValue,
                    Status = tripDTO.Status
                };

                var result = await _tripService.UpdateTripAsync(trip);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the trip: {ex.Message}");
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateTrip(int id)
        {
            try
            {
                await _tripService.DeactivateTripAsync(id);
                return Ok("Trip deactivated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateTrip(int id)
        {
            try
            {
                await _tripService.ActivateTripAsync(id);
                return Ok("Trip activated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
