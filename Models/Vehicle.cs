using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public string? LicenseNo { get; set; }
        public DateTime LicenseExpireDate { get; set; }
        public string? VehicleColor { get; set; }
        public string? Status { get; set; }
        
        // Vehicle_Type
        public int VehicleTypeId { get; set; }
        public VehicleType? Type { get; set; }
        
        // Vehicle_Manufacture
        public int ManufactureId { get; set; }
        public Manufacture? Manufacturer { get; set; }
        
        // Accident
        public int? AccidentId { get; set; }
        public Accident? Accident { get; set; }
        
        // Trip
        public string? TripId { get; set; }
        public Trip? Trip { get; set; }
        
        // Vehicle_Maintenance
        public ICollection<VehicleMaintenance> VehicleMaintenances { get; set; } // Corrected to plural
        
        // FuelRefill
        public ICollection<FuelRefill> FuelRefills { get; set; }
    }
}