using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetPulse_BackEndDevelopment.Models.Configurations
{
    public class AccidentUserConfig : IEntityTypeConfiguration<AccidentUser>
    {
        public void Configure(EntityTypeBuilder<AccidentUser> builder)
        {
            builder.HasKey(au => new { au.UserId, au.AccidentId });

            builder.HasOne<User>(au => au.User)
                .WithMany(u => u.AccidentUsers)
                .HasForeignKey(au => au.UserId);

            builder.HasOne<Accident>(au => au.Accident)
                .WithMany(t => t.AccidentUsers)
                .HasForeignKey(au => au.AccidentId);
        }
    }
}