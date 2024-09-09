using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class TripUserDTO
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
