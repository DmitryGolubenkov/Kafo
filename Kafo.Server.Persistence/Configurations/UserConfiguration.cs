using Kafo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kafo.Server.Persistence.Configurations;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Username).IsRequired();
        builder.Property(b => b.PasswordHash).IsRequired();
        builder.Property(b => b.PasswordSalt).IsRequired();
    }
}