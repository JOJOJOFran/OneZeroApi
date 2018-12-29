using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnzeZero.Entity.Configuration
{
    public class RoleConfiguration:IEntityTypeConfiguration<Role>
    {
        public  void Configure(EntityTypeBuilder<Role> builder)
        {
            throw new NotImplementedException();
        }
    }
}
