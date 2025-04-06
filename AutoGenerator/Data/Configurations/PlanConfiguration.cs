using AutoGenerator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoGenerator.Data.Configurations;

internal sealed class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder
        .HasMany(u => u.Subscriptions)
        .WithOne(u => u.Plan)
        .HasForeignKey(s => s.PlanId)
        .IsRequired(false)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(p => p.PlanFeatures).AutoInclude();
    }
}
