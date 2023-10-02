using DataAccess.Entities;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.DbContext.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder
            .HasOne(u => u.RefreshToken)
            .WithOne(t => t.User)
            .HasForeignKey<RefreshToken>(t => t.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
    }
}