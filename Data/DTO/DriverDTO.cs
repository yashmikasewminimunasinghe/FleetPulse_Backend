using FleetPulse_BackEndDevelopment.Models;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.DTOs
{
    public class DriverDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NIC { get; set; }
        public string DriverLicenseNo { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string EmergencyContact { get; set; }
        public string BloodGroup { get; set; }
        public bool Status { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; } 
        
    }
}
