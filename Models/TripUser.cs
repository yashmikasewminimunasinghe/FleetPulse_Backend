namespace FleetPulse_BackEndDevelopment.Models
{
    public class TripUser
    {
        public int TripUserId { get; set; } // Assuming TripUserId is the primary key
        public int UserId { get; set; }
        public User User { get; set; }
        public string TripId { get; set; }
        public Trip Trip { get; set; }
        public bool Status { get; set; } // Adding the Status property
    }
}
