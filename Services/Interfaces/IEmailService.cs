using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}