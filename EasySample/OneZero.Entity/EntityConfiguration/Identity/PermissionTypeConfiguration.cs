using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnzeZero.Entity.Configuration
{
    public class PermissionTypeConfiguration:IEntityTypeConfiguration<PermissionType>
    {
        public  void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.ToTable("TPermissionType");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => v.Name);
        }
    }
}