using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Application.Models.Permissions;

namespace OneZero.Application.EntityConfiguration.Permission
{
    public class ModulePermissionConfiguration : IEntityTypeConfiguration<ModulePermission>
    {
        public void Configure(EntityTypeBuilder<ModulePermission> builder)
        {
            builder.ToTable("TModulePermission");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => new { v.PermissionId, v.ModuleId });
            builder.HasIndex(v => v.SeqNo);
        }
    }
}
