using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Core.Models.Permissions;


namespace OneZero.Core.EntityConfiguration.Permission
{
    public class ModuleTypeConfiguration : IEntityTypeConfiguration<ModuleType>
    {
        public void Configure(EntityTypeBuilder<ModuleType> builder)
        {
            builder.ToTable("TModuleType");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.ParentId).IsRequired(false);
            builder.HasIndex(v => new { v.Name,v.IsDelete, v.TenanId }).IsUnique();
        }


    }
}
