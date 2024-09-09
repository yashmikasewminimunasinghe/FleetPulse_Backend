using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class DeviceTokenService : IDeviceTokenService
    {
        private readonly FleetPulseDbContext _context;

        public DeviceTokenService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeviceToken>> GetAllTokensAsync()
        {
            return await _context.DeviceTokens.ToListAsync();
        }

        public async Task AddTokenAsync(DeviceToken token)
        {
            token.CreatedAt = DateTime.UtcNow;
            _context.DeviceTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task<DeviceToken> GetTokenByIdAsync(int id)
        {
            return await _context.DeviceTokens.FindAsync(id);
        }
    }
}