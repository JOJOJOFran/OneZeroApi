using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Enums;

namespace OneZero.EntityFrameworkCore.SqlServer
{
    public class DbContextOptionBuilderCreator : IDbContextOptionBuilderCreator
    {
        public DatabaseType Type { get;  }= DatabaseType.SqlServer;

        public DbContextOptionsBuilder Create(string connectionString, string MigrationAssmblyName, ILoggerFactory loggerFactory)
        {
            return new DbContextOptionsBuilder().UseLoggerFactory(loggerFactory)
                                                .UseSqlServer(connectionString, b => b.MigrationsAssembly(MigrationAssmblyName));
        }
    }
}
