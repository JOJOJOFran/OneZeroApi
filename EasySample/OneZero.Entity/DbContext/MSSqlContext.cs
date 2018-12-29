using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace OneZero.Entity.DbContext
{
    public class MSSqlContext:IDbContext,DbContext
    {       
        protected override void OnModelBuildeing(ModelBuilder modelBuilder)
        {
            
        }
    }
}