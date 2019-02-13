using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Application.Models.Permissions;

namespace OneZero.Application.EntityConfiguration.Permission
{
    public class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.ToTable("TPermissionType");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => v.Name);
        }
    }
}
