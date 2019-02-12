using System;
using System.Collections.Generic;

namespace OneZero.Core
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class OneZeroOption
    {
        public DbContextOption dbContextOption { get; set; }
        public JwtOption jwtOption { get; set; }
    }

    /// <summary>
    /// JWT配置
    /// </summary>
    public class JwtOption
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        //需要大于128字节，所以一般超过16个字符
        public string SecretKey { get; set; }
    }

    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbContextOption
    {
        public string ConnectString { get; set; }

        public DatabaseType DBType;

        public Type DbContextType;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public  Dictionary<Type,Type> EntityList { get; set; }
    }

    public enum DatabaseType
    {
        SqlServer,
        MySql,
        PgSql,
        MongoDb
    }
}
