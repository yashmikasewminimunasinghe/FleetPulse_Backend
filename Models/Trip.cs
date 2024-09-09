using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class Trip
    {
       
        [Key]
        public string TripId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
<<<<<<< HEAD
        public TimeSpan EndTime { get; set; }
        
=======
        public TimeSpan EndTime { get; set; }        
>>>>>>> 5579fc1592e354896cc3901e35233f547c26d873
        public float StartMeterValue { get; set; }
        public float EndMeterValue { get; set; }
        public bool Status { get; set; }
        
        //Vehicle
        public ICollection<Vehicle> Vehicles { get; set; }
        //TripUser
        public ICollection<TripUser> TripUsers { get; set; }

    }
}