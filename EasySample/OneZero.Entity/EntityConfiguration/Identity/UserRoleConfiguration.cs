using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnzeZero.Entity.Configuration
{
    public class UserRoleConfiguration:IEntityTypeConfiguration<UserRole>
    {
        public  void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("TUserRole");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => new { v.RoleId, v.UserId });


        }
    }
}
