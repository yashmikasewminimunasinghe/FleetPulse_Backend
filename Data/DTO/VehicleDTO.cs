using FleetPulse_BackEndDevelopment.Models;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class VehicleDTO
    {
        public int VehicleId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseExpireDate { get; set; }
        public string VehicleColor { get; set; }
        public string? Status { get; set; }
        public int VehicleModelId { get; set; }
        public int VehicleTypeId { get; set; }
        public int ManufactureId { get; set; }
        public int? AccidentId { get; set; }
        public string TripId { get; set; }
        public ICollection<int>? FuelRefillIds { get; set; }
        public ICollection<int>? VehicleMaintenanceIds { get; set; }
    }
}
