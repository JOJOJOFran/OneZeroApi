using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnzeZero.EntityFramwork.Configuration
{
    public class RoleModuleConfiguration : IEntityTypeConfiguration<RoleModule>
    {
        public void Configure(EntityTypeBuilder<RoleModule> builder)
        {
            builder.ToTable("TRoleModule");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => new { v.RoleId, v.ModleId });

            builder.Property(v => v.PermissionValue).HasDefaultValue(0);
        }
    }
}