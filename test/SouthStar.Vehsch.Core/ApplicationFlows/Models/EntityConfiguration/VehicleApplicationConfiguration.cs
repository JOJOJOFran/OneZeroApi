using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.ApplicationFlow.Models.EntityConfiguration
{
    public class VehicleApplicationConfiguration : IEntityTypeConfiguration<VehicleApplications>
    {
        public void Configure(EntityTypeBuilder<VehicleApplications> modelBuilder)
        {
            modelBuilder.ToTable("TVehicleApplications").HasKey(v => v.Id);
            modelBuilder.HasIndex(v =>new { v.ApplyNum ,v.IsDelete,v.TenanId}).IsUnique();
        }
    }
}
