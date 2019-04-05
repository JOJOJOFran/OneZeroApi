using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Setting.Models.EntityConfiguration
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicles>
    {
        public void Configure(EntityTypeBuilder<Vehicles> modelBuilder)
        {
            modelBuilder.ToTable("TVehicles").HasKey(v => v.Id);
            modelBuilder.HasIndex(v => new { v.PlateNumber, v.IsDelete, v.TenanId }).IsUnique();
            modelBuilder.HasIndex(v => new { v.EngineNo, v.IsDelete, v.TenanId }).IsUnique();
            modelBuilder.HasIndex(v => new { v.VIN, v.IsDelete, v.TenanId }).IsUnique();
        }
    }
}
