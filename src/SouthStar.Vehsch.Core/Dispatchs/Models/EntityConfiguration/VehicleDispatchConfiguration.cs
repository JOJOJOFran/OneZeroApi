using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Dispatch.Models.EntityConfiguration
{
    public class VehicleDispatchConfiguration : IEntityTypeConfiguration<VehicleDispatchs>
    {
        public void Configure(EntityTypeBuilder<VehicleDispatchs> modelBuilder)
        {
            modelBuilder.ToTable("TVehicleDispatchs").HasKey(v => v.Id);
            modelBuilder.HasIndex(v => new { v.ApplyNum ,v.TenanId, v.IsDelete}).IsUnique();
        }
    }
}
