namespace FleetPulse_BackEndDevelopment.Models;

public class ResetDriverPasswordRequest
{
    public string EmailAddress { get; set; }
    public string NewPassword { get; set; }
}