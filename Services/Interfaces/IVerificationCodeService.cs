using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces;

public interface IVerificationCodeService
{
    Task<VerificationCode> GenerateVerificationCode(string email);
    Task<bool> ValidateVerificationCode(ValidateVerificationCodeRequest request);
    Task<bool> ValidateVerificationCode(string email, string code);
}
