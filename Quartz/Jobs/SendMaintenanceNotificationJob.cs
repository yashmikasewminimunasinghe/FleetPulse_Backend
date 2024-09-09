using FleetPulse_BackEndDevelopment.Services;
using Quartz;
using FleetPulse_BackEndDevelopment.Services.Interfaces;

namespace FleetPulse_BackEndDevelopment.Quartz.Jobs
{
    public class SendMaintenanceNotificationJob : IJob
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IVehicleMaintenanceConfigurationService _vehicleMaintenanceConfigurationService;
        private readonly IDeviceTokenService _deviceTokenService;
        private readonly ILogger<SendMaintenanceNotificationJob> _logger;

        public SendMaintenanceNotificationJob(
            IPushNotificationService pushNotificationService,
            IVehicleMaintenanceConfigurationService vehicleMaintenanceConfigurationService,
            IDeviceTokenService deviceTokenService,
            ILogger<SendMaintenanceNotificationJob> logger)
        {
            _pushNotificationService = pushNotificationService ?? throw new ArgumentNullException(nameof(pushNotificationService));
            _vehicleMaintenanceConfigurationService = vehicleMaintenanceConfigurationService ?? throw new ArgumentNullException(nameof(vehicleMaintenanceConfigurationService));
            _deviceTokenService = deviceTokenService ?? throw new ArgumentNullException(nameof(deviceTokenService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var dueTasks = await _vehicleMaintenanceConfigurationService.GetDueMaintenanceTasksAsync();

                if (dueTasks.Count == 0)
                {
                    _logger.LogInformation("No maintenance tasks are due.");
                    return;
                }

                var deviceTokens = await _deviceTokenService.GetAllTokensAsync();

                foreach (var task in dueTasks)
                {
                    var message = $"Vehicle {task.VehicleId} requires maintenance for {task.TypeName}.";

                    foreach (var token in deviceTokens)
                    {
                        await _pushNotificationService.SendNotificationAsync(token.Token, "Maintenance Due", message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending maintenance notifications.");
                throw;
            }
        }
    }
}
