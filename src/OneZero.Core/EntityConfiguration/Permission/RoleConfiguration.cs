using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Core.Models.Permissions;

namespace OneZero.Core.EntityConfiguration.Permission
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("TRole");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v =>new { v.Name , v.IsDelete, v.TenanId }).IsUnique();
            builder.OwnsMany(v => v.RolePermission);
            builder.OwnsMany(v => v.RoleModules);
        }
    }
}
