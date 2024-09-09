namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class VehicleDetails
    {
        public int VehicleId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseExpireDate { get; set; }
        public string VehicleColor { get; set; }
        public string Status { get; set; }
        public string VehicleModle { get; set; }
        public string VehicleType { get; set; }
        public string Manufacture { get; set; }
        public string Fuel { get; set; }
        public string VehicleMaintenance { get; set; }
    }
}
