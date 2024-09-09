using FleetPulse_BackEndDevelopment.Models;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class VehicleTypeDTO
    {
        [Key]
        public int VehicleTypeId { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }
}
