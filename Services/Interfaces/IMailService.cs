using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces;

public interface IMailService
{ 
    Task SendEmailAsync(MailRequest mailRequest);
}