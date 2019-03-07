using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Models.EntityConfiguration
{
    public class DriverConfiguration : IEntityTypeConfiguration<Drivers>
    {
        public void Configure(EntityTypeBuilder<Drivers> modelBuilder)
        {
            modelBuilder.ToTable("TDrivers").HasKey(v => v.Id);
            modelBuilder.HasIndex(v => v.DrivingLicNum).IsUnique();
            modelBuilder.HasIndex(v => v.PhoneNum).IsUnique();
        }
    }
}
