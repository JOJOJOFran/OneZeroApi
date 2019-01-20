using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnzeZero.EntityFramwork.Configuration
{
    public class ModulePermissionConfiguration:IEntityTypeConfiguration<ModulePermission>
    {
        public  void Configure(EntityTypeBuilder<ModulePermission> builder)
        {
            builder.ToTable("TModulePermission");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => new { v.PermissionId, v.ModuleId });
            builder.HasIndex(v => v.SeqNo);
        }
    }
}
