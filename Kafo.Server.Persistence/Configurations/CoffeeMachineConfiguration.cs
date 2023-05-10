using Kafo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kafo.Server.Persistence.Configurations;

internal class CoffeeMachineConfiguration : IEntityTypeConfiguration<CoffeeMachineModel>
{
    public void Configure(EntityTypeBuilder<CoffeeMachineModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(b => b.ManufacturerName).IsRequired();
        builder.Property(b => b.Model).IsRequired();
    }
}