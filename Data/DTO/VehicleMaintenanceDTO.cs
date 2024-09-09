namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class VehicleMaintenanceDTO
    {
        public int MaintenanceId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public decimal Cost { get; set; }
        public string PartsReplaced { get; set; }
        public string ServiceProvider { get; set; }
        public string SpecialNotes { get; set; }
        public int VehicleId { get; set; }
        public string? VehicleRegistrationNo { get; set; }
        public int VehicleMaintenanceTypeId { get; set; }
        public string? TypeName { get; set; }

        public bool Status { get; set; }
    }
}

