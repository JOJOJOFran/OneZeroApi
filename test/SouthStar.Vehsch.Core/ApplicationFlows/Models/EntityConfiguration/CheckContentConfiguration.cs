using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.ApplicationFlow.Models.EntityConfiguration
{
    public class CheckContentConfiguration : IEntityTypeConfiguration<CheckContents>
    {
        public void Configure(EntityTypeBuilder<CheckContents> modelBuilder)
        {
            modelBuilder.ToTable("TCheckContents").HasKey(v => v.Id);
            modelBuilder.HasIndex(v => new { v.ApplyNum ,v.IsDelete, v.TenanId }).IsUnique();
        }
    }
}
