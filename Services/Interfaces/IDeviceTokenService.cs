using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IDeviceTokenService
    {
        Task<IEnumerable<DeviceToken>> GetAllTokensAsync();
        Task AddTokenAsync(DeviceToken token);
        Task<DeviceToken> GetTokenByIdAsync(int id);
    }
}