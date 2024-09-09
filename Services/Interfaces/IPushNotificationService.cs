using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Models.FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IPushNotificationService
    {
        Task SendNotificationAsync(string fcmDeviceToken, string title, string message);
        Task<List<FCMNotification>> GetAllNotificationsAsync();
        Task MarkNotificationAsReadAsync(string notificationId);
        Task MarkAllAsReadAsync();
        Task DeleteNotificationAsync(string notificationId);
        Task DeleteAllNotificationsAsync();
        Task SendMaintenanceNotificationAsync();
    }
}