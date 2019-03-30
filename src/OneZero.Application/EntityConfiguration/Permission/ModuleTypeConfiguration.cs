using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Application.Models.Permissions;


namespace OneZero.Application.EntityConfiguration.Permission
{
    public class ModuleTypeConfiguration : IEntityTypeConfiguration<ModuleType>
    {
        public void Configure(EntityTypeBuilder<ModuleType> builder)
        {
            builder.ToTable("TModuleType");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.ParentId).IsRequired(false);
            builder.HasIndex(v => v.Name);
        }


    }
}
