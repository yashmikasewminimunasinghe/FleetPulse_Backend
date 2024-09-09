namespace FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using System.ComponentModel.DataAnnotations;

public class StaffDTO
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NIC { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNo { get; set; }
    public string EmailAddress { get; set; }
    public string ProfilePicture { get; set; }
    public string EmergencyContact { get; set; }
    public string? JobTitle { get; set; } // Specific to Staff
    public bool Status { get; set; }
    public string Password { get; set; }
}

