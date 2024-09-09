using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class VehicleType
    {
        [Key]
        public int VehicleTypeId { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }

        //Vehicle
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}