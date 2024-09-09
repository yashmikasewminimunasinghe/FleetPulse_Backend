using FleetPulse_BackEndDevelopment.Models;
using Google.Apis.Auth.OAuth2.Responses;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IAuthService
    {
        User IsAuthenticated(string username, string password);
        bool DoesUserExists(string username);
        bool DoesEmailExists(string email);
        string GetUsernameByEmail(string email);
        User GetById(int id);
        User[] GetAll();
        User GetByUsername(string username);
        User RegisterUser(User model);
        string DecodeEmailFromToken(string token);
        User ChangeRole(string username, string jobTitle);
        User GetByEmail(string email);
        bool ResetPassword(string email, string newPassword);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeactivateUserAsync(User user);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
        Task<bool> ResetDriverPasswordAsync(string emailAddress, string newPassword);
        Task<int?> GetUserIdByNICAsync(string nic);
        Task<TokenResponse> Authenticate(User user);
        Task<TokenResponse> RefreshToken(string token);
        Task<bool> IsRefreshTokenValidAsync(string token);
        Task<bool> RevokeToken(string token);
        Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
        Task<bool> RevokeRefreshTokenAsync(string token);
        Task<string> GenerateJwtToken(string username, string jobTitle);
        Task<string> GenerateRefreshToken(int userId);
        Task<bool> ValidateRefreshToken(string token);
    }
}