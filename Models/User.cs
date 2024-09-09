using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NIC { get; set; }
    public string? DriverLicenseNo { get; set; }
    public DateTime? LicenseExpiryDate { get; set; }
    public string? BloodGroup { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNo { get; set; }
    public string UserName { get; set; }
    public string HashedPassword { get; set; }
    public string EmailAddress { get; set; }
    public string EmergencyContact { get; set; }
    public string JobTitle { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public bool Status { get; set; }
    public IList<FuelRefill> FuelRefills { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public ICollection<AccidentUser> AccidentUsers { get; set; }
    public ICollection<TripUser> TripUsers { get; set; }
    public ICollection<FuelRefillUser> FuelRefillUsers { get; set; }
}