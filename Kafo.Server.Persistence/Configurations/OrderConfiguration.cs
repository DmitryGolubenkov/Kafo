using Kafo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kafo.Server.Persistence.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.CoffeeMachine).WithMany(x => x.Orders).HasForeignKey(x => x.CoffeeMachineId);
    }
}