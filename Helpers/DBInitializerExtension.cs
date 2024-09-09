using FleetPulse_BackEndDevelopment.Data;

namespace FleetPulse_BackEndDevelopment.Helpers;

public static class DBInitializerExtension
{
    public static IApplicationBuilder UseSeedDB(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<FleetPulseDbContext>();
        DBSeeder.Seed(context);

        return app;
    }
}