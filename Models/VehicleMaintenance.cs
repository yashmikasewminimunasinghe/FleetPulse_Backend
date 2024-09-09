using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class VehicleMaintenance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaintenanceId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public decimal Cost { get; set; }
        public string PartsReplaced { get; set; }
        public string ServiceProvider { get; set; }
        public string SpecialNotes { get; set; }
        public bool Status { get; set; }
        
        // Vehicle
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        
        // Vehicle_Maintenance_Type
        public int VehicleMaintenanceTypeId { get; set; }
        public VehicleMaintenanceType VehicleMaintenanceType { get; set; }
    }
}