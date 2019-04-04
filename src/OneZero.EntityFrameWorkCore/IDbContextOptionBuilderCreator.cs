using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Enums;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace OneZero.EntityFrameworkCore
{
    public interface IDbContextOptionBuilderCreator
    {
        /// <summary>
        /// 获取 数据库类型名称，如SqlServer，MySql，Sqlite等
        /// </summary>
        DatabaseType Type { get; }

        /// <summary>
        /// 创建数据库连接配置对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="MigrationAssmblyName"></param>
        /// <param name="loggerFactory"></param>
        /// <returns></returns>
        DbContextOptionsBuilder Create( string connectionString, string MigrationAssmblyName, ILoggerFactory loggerFactory);

    }
}
