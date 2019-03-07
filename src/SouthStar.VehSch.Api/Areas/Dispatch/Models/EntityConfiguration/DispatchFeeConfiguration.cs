using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Models.EntityConfiguration
{
    public class DispatchFeeConfiguration : IEntityTypeConfiguration<DispatchFees>
    {
        public void Configure(EntityTypeBuilder<DispatchFees> modelBuilder)
        {
            modelBuilder.ToTable("TDispatchFees").HasKey(v => v.Id);
            modelBuilder.HasIndex(v => v.DispatchId).IsUnique();
        }
    }
}
