using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models;

public class WebNotification
{
    [Key]
    public string NotificationId { get; set; }
    
    public string JobTitle { get; set; }
    
    public string Title { get; set; }
    
    public string Message { get; set; }
    
    public DateTime Date { get; set; }
    
    public TimeSpan Time { get; set; }
    
    public bool IsMarked { get; set; }
}