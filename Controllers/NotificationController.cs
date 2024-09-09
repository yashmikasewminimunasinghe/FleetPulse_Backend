using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Models.FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IConfiguration _configuration;

        public NotificationController(IPushNotificationService pushNotificationService, IConfiguration configuration)
        {
            _pushNotificationService = pushNotificationService;
            _configuration = configuration;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FCMNotification>>> GetNotifications()
        {
            var notifications = await _pushNotificationService.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        [HttpPost("mark-as-read/{id}")]
        public async Task<IActionResult> MarkNotificationAsRead(string id)
        {
            await _pushNotificationService.MarkNotificationAsReadAsync(id);
            return Ok(new { Status = "Success", Message = "Notification marked as read" });
        }
        
        [HttpPost("markAllAsRead")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            await _pushNotificationService.MarkAllAsReadAsync();
            return Ok(new { Status = "Success", Message = "All notifications marked as read" });
        }

        [HttpDelete("delete{id}")]
        public async Task<IActionResult> DeleteNotification(string id)
        {
            await _pushNotificationService.DeleteNotificationAsync(id);
            return Ok(new { Status = "Success", Message = "Notification deleted successfully" });
        }
        
        [HttpDelete("deleteAll")]
        public async Task<IActionResult> DeleteAllNotifications()
        {
            await _pushNotificationService.DeleteAllNotificationsAsync();
            return Ok(new { Status = "Success", Message = "All notifications deleted" });
        }
    }
}
