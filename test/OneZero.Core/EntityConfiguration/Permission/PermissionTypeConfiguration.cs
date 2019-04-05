using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Core.Models.Permissions;

namespace OneZero.Core.EntityConfiguration.Permission
{
    public class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.ToTable("TPermissionType");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v =>new { v.Name, v.IsDelete, v.TenanId }).IsUnique();
        }
    }
}
