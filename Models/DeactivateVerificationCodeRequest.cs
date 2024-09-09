namespace FleetPulse_BackEndDevelopment.Models;

public class DeactivateVerificationCodeRequest
{
    public string Email { get; set; }
    public string Code { get; set; }
}