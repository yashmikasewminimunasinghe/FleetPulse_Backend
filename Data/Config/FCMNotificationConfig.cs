using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Models.FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class FCMNotificationConfig : IEntityTypeConfiguration<FCMNotification>
    {
        public void Configure(EntityTypeBuilder<FCMNotification> builder)
        {
            builder.HasKey(n => n.NotificationId);

            builder.Property(n => n.Title).IsRequired().HasMaxLength(255); 
            builder.Property(n => n.Message).IsRequired().HasMaxLength(1000);
            builder.Property(n => n.Date).IsRequired();
            builder.Property(n => n.Time).IsRequired();
            builder.Property(n => n.Status).IsRequired();

            builder.ToTable("Notifications");
        }
    }
}