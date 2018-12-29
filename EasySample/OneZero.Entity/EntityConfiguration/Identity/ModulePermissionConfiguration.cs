using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnzeZero.Entity.Configuration
{
    public class ModulePermissionConfiguration:IEntityTypeConfiguration<ModulePermission>
    {
        public  void Configure(EntityTypeBuilder<ModulePermission> builder)
        {
            throw new NotImplementedException();
        }
    }
}
