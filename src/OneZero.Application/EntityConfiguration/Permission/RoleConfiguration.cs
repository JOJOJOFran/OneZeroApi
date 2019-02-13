using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Application.Models.Permissions;

namespace OneZero.Application.EntityConfiguration.Permission
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("TRole");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => v.Name);
        }
    }
}
