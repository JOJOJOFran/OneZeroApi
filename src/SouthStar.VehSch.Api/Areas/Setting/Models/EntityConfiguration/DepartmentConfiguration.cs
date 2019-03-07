using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Models.EntityConfiguration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Departments>
    {
        public void Configure(EntityTypeBuilder<Departments> modelBuilder)
        {
            modelBuilder.ToTable("TDepartments").HasKey(v => v.Id);
            modelBuilder.HasIndex(v => v.DepartmentName).IsUnique();
        }
    }
}
