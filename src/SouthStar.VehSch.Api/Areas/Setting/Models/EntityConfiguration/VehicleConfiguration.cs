using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Models.EntityConfiguration
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicles>
    {
        public void Configure(EntityTypeBuilder<Vehicles> modelBuilder)
        {
            modelBuilder.ToTable("TVehicles").HasKey(v => v.Id);
            modelBuilder.HasIndex(v => v.PlateNumber).IsUnique();
            modelBuilder.HasIndex(v => v.EngineNo).IsUnique();
        }
    }
}
