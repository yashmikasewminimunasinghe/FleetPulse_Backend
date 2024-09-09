using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetPulse_BackEndDevelopment.Data.Config;

public class VerificationCodeConfig : IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.ToTable("VerificationCodes");

        builder.HasKey(vc => vc.Id);
        builder.Property(vc => vc.Code).IsRequired();
        builder.Property(vc => vc.IsActive).IsRequired();
        builder.Property(vc => vc.ExpirationDateTime).IsRequired();
        builder.Property(vc => vc.Email).IsRequired().HasMaxLength(255);
    }
}