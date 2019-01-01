using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnzeZero.Entity.Configuration
{
    public class ModuleTypeConfiguration:IEntityTypeConfiguration<ModuleType>
    {
        public  void Configure(EntityTypeBuilder<ModuleType> builder)
        {
            builder.ToTable("TModuleType");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => v.Name);
        }

        
    }
}
