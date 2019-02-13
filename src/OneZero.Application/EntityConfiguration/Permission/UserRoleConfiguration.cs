﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Application.Models.Permissions;

namespace OneZero.Application.EntityConfiguration.Permission
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("TUserRole");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => new { v.RoleId, v.UserId });
        }
    }
}
