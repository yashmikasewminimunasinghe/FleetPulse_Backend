using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Data.DTO;

public class VehicleMaintenanceTypeDTO
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public bool Status { get; set; }
}