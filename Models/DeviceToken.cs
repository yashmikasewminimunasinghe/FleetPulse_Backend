using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models;

public class DeviceToken
{
    [Key]
    public int Id { get; set; }
        
    [Required]
    public string Token { get; set; }
        
    [Required]
    public DateTime CreatedAt { get; set; }
}