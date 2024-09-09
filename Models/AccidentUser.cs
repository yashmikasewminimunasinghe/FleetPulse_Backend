using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class AccidentUser
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }

        public int AccidentId { get; set; }
        public Accident Accident { get; set; }

        public bool Status { get; set; }
    }
}
