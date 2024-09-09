using FleetPulse_BackEndDevelopment.Models;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class AccidentUserDTO
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }

        public int AccidentId { get; set; }
        public Accident Accident { get; set; }

       
    }
}
