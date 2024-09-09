using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace FleetPulse_BackEndDevelopment.Models
    {
        public class FCMNotification
        {
            [Key] public string NotificationId { get; set; }

            [Required] public string UserName { get; set; }

            [Required] public string Title { get; set; }

            [Required] public string Message { get; set; }

            public DateTime Date { get; set; }

            public TimeSpan Time { get; set; }

            public bool Status { get; set; }
        }
    }
}