﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Application.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Application.EntityConfiguration.Permission
{
    public class RoleModuleConfiguration : IEntityTypeConfiguration<RoleModule>
    {
        public void Configure(EntityTypeBuilder<RoleModule> builder)
        {
            builder.ToTable("TRoleModule");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => new { v.RoleId, v.ModuleId });
            
        }
    }
}
