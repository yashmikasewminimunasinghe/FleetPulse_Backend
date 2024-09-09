using FleetPulse_BackEndDevelopment.Models;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class ManufactureDTO
    {
        [Key]
        public int ManufactureId { get; set; }
        public string Manufacturer { get; set; }
        public bool Status { get; set; }
    }
}
