using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Core.Models.Permissions;

namespace OneZero.Core.EntityConfiguration.Permission
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("TRolePermission");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => new { v.PermissionId, v.RoleId, v.TenanId }).IsUnique();
            builder.HasIndex(v => v.SeqNo);
        }
    }
}
