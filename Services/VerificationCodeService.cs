using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services;

public class VerificationCodeService : IVerificationCodeService
{
    private readonly FleetPulseDbContext _context;
    private IVerificationCodeService _verificationCodeServiceImplementation;

    public VerificationCodeService(FleetPulseDbContext context)
    {
        _context = context;
    }

    public async Task<VerificationCode> GenerateVerificationCode(string email)
    {
        var verificationCode = new VerificationCode
        {
            Code = GenerateRandomCode(),
            IsActive = true,
            ExpirationDateTime = DateTime.Now.AddMinutes(5),
            Email = email
        };

        _context.VerificationCodes.Add(verificationCode);
        await _context.SaveChangesAsync();

        return verificationCode;
    }

    private string GenerateRandomCode()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString();
    }

    public async Task<bool> ValidateVerificationCode(ValidateVerificationCodeRequest request)
    {
        return await ValidateVerificationCode(request.Email, request.Code);
    }

    public async Task<bool> ValidateVerificationCode(string email, string code)
    {
        
        var verificationCode = await _context.VerificationCodes
            .FirstOrDefaultAsync(vc =>
                vc.Email == email && vc.Code == code && vc.IsActive);
        if (verificationCode != null && verificationCode.ExpirationDateTime > DateTime.Now)
        {
            return true;
        }
        return false;
       }
    
}