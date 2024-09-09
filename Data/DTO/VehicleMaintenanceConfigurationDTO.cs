namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class VehicleMaintenanceConfigurationDTO
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public int VehicleMaintenanceTypeId { get; set; }
        public string TypeName { get; set; }
        public string Duration { get; set; }
        public bool Status { get; set; }
    }
}